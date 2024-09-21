using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024_09_21/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var categories = await _categoryService.GetAllAsync();
            var mapCategories = categories.Select(category => new GetCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            }).ToList();
            var totalCategories = mapCategories.Count();
            int totalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);
            var paginatedCategories = mapCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationMetadata = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalProducts = totalCategories
            };

            return Ok(new
            {
                Products = paginatedCategories,
                Pagination = paginationMetadata
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            var mapCategory = new GetCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            return Ok(mapCategory);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromQuery] AddCategoryDto category)
        {
            if (category == null)
                return BadRequest(new { Message = $"Please fill in field." });

            var categoryModel = new Category
            {
                CategoryName = category.CategoryName
            };

            await _categoryService.AddAsync(categoryModel);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto category)
        {
            if (category == null)
                return BadRequest(new { Message = $"Please fill in field." });

            var p = await _categoryService.GetByIdAsync(category.CategoryId);
            if (p == null)
                return NotFound(new { Message = $"Category with ID {category.CategoryId} not found." });

            if (!string.IsNullOrEmpty(category.CategoryName))
                p.CategoryName = category.CategoryName;

            await _categoryService.UpdateAsync(p);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("Include")]
        public async Task<ActionResult<IEnumerable<GetCategoryIncludeDto>>> GetAllCategoriesWithInclude(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var categories = await _categoryService.GetAllIncludeAsync();
            var mapCategories = categories.Select(category => new GetCategoryIncludeDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products != null
                    ? category.Products.Select(product => new GetProductDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        CategoryId = product.CategoryId,
                        UnitsInStock = product.UnitsInStock,
                        UnitPrice = product.UnitPrice
                    }).ToList()
                    : new List<GetProductDto>()
            }).ToList();
            var totalCategories = mapCategories.Count();
            int totalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);
            var paginatedCategories = mapCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationMetadata = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCategories = totalCategories
            };

            return Ok(new
            {
                Categories = paginatedCategories,
                Pagination = paginationMetadata
            });
        }

        [HttpGet("Include/{id}")]
        public async Task<ActionResult<GetCategoryIncludeDto>> GetCategoryByIdWithInclude(int id)
        {
            var category = await _categoryService.GetCategoryByIdIncludeAsync(id);

            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            var mapCategory = new GetCategoryIncludeDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products != null
                    ? category.Products.Select(product => new GetProductDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        CategoryId = product.CategoryId,
                        UnitsInStock = product.UnitsInStock,
                        UnitPrice = product.UnitPrice
                    }).ToList()
                    : new List<GetProductDto>()
            };

            return Ok(mapCategory);
        }

    }
}
