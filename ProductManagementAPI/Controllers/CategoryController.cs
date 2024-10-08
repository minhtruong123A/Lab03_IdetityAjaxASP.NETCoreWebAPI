﻿using BusinessObjects.Dtos.Categories;
using Helper.Pagination;
using Helper.Schema_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024-09-21/categories")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet("public-info")]
        [AllowAnonymous]
        public IActionResult GetPublicInfo() => Ok("This is public information.");

        [HttpGet]
        public async Task<ActionResult> GetAllCategories(
                    int pageNumber = 1, int pageSize = 10, string? nameFilter = null, [FromQuery] List<int>? categoryIds = null)
        {
            var result = await _categoryService.GetAllAsync(nameFilter, categoryIds, pageNumber, pageSize);
            return Ok(new ResponseModel<PaginatedResult<GetCategoryDto>> { Data = result, Success = true });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return category == null ? NotFound(new ResponseModel<GetCategoryDto> { Success = false, Error = $"Category with ID {id} not found.", ErrorCode = 404 })
                                    : Ok(new ResponseModel<GetCategoryDto> { Data = category });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromQuery] AddCategoryDto category)
        {
            if (category == null) return BadRequest(new ResponseModel<object> { Success = false, Error = "Please fill in field.", ErrorCode = 400 });
            await _categoryService.AddAsync(category);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto category)
        {
            if (category == null) return BadRequest(new ResponseModel<object> { Success = false, Error = "Please fill in the required field.", ErrorCode = 400 });
            try { await _categoryService.UpdateAsync(category); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try { await _categoryService.DeleteAsync(id); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }
        }

        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<GetCategoryIncludeDto>>> GetAllCategoriesWithInclude(
                    int pageNumber = 1, int pageSize = 10, string? nameFilter = null, [FromQuery] List<int>? categoryIds = null)
        {
            var result = await _categoryService.GetAllIncludeAsync(nameFilter, categoryIds, pageNumber, pageSize);
            return Ok(new ResponseModel<PaginatedResult<GetCategoryIncludeDto>> { Data = result, Success = true });
        }

        [HttpGet("include/{id}")]
        public async Task<ActionResult<GetCategoryIncludeDto>> GetCategoryByIdWithInclude(int id)
        {
            var category = await _categoryService.GetCategoryByIdIncludeAsync(id);
            return category == null ? NotFound(new ResponseModel<GetCategoryIncludeDto> { Success = false, Error = $"Category with ID {id} not found.", ErrorCode = 404 })
                                    : Ok(new ResponseModel<GetCategoryIncludeDto> { Data = category });
        }
    }
}
