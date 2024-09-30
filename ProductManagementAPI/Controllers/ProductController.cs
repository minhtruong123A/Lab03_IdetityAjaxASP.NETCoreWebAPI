using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024-09-21/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetAllAsync(pageNumber, pageSize);
            return Ok(new {
                Products = result.Items,
                Pagination = new
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    TotalProducts = result.TotalCount
                }});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound(new { Message = $"Product with ID {id} not found." });
            return Ok(product);
        }

        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<GetProductsIncludeDto>>> GetAllProductsWithInclude(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetAllIncludeAsync(pageNumber, pageSize);
            return Ok(new {
                Products = result.Items,
                Pagination = new
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    TotalProducts = result.TotalCount
                }});
        }

        [HttpGet("include/{id}")]
        public async Task<ActionResult<GetProductsIncludeDto>> GetProductByIdWithInclude(int id)
        {
            var product = await _productService.GetByIdIncludeAsync(id);
            if (product == null) return NotFound(new { Message = $"Product with ID {id} not found." });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto p)
        {
            if (p == null) return BadRequest(new { Message = "Please fill in field." });
            await _productService.AddAsync(p);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product)
        {
            if (product == null) return BadRequest(new { Message = "Please fill in field." });
            try {
                await _productService.UpdateAsync(product);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try {
                await _productService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
