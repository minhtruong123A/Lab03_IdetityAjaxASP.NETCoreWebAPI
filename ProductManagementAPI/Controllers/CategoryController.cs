using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024-09-21/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _categoryService.GetAllAsync(pageNumber, pageSize);
            return Ok(new {
                Categories = result.Items,
                Pagination = new
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    TotalCategories = result.TotalCount
                }});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound(new { Message = $"Category with ID {id} not found." });
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromQuery] AddCategoryDto category)
        {
            if (category == null) return BadRequest(new { Message = $"Please fill in field." });
            await _categoryService.AddAsync(category);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto category)
        {
            if (category == null) return BadRequest(new { Message = "Please fill in the required field." });
            try{
                await _categoryService.UpdateAsync(category);
                return NoContent();
            }catch (KeyNotFoundException ex){
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<GetCategoryIncludeDto>>> GetAllCategoriesWithInclude(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _categoryService.GetAllIncludeAsync(pageNumber, pageSize);
            return Ok(new {
                Categories = result.Items,
                Pagination = new {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    TotalCategories = result.TotalCount
                }});
        }

        [HttpGet("include/{id}")]
        public async Task<ActionResult<GetCategoryIncludeDto>> GetCategoryByIdWithInclude(int id)
        {
            var category = await _categoryService.GetCategoryByIdIncludeAsync(id);
            if (category == null) return NotFound(new { Message = $"Category with ID {id} not found." });
            return Ok(category);
        }
    }
}
