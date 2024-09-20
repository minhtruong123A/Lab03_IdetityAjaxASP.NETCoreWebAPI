using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var products = await _categoryService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //[HttpGet("Include")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsWithInclude()
        //{
        //    var products = await _categoryService.GetAllIncludeAsync();
        //    return Ok(products);
        //}

        //[HttpGet("Include/{id}")]
        //public async Task<ActionResult<Product>> GetProductByIdWithInclude(int id)
        //{
        //    var product = await _categoryService.GetByIdIncludeAsync(id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            var createdProduct = await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdProduct.CategoryId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || id != category.CategoryId)
            {
                return BadRequest();
            }

            await _categoryService.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
