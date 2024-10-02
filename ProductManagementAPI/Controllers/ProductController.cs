using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Helper;
using Helper.Pagination;
using Helper.Schema_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024-09-21/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet("public-info")]
        [AllowAnonymous]
        public IActionResult GetPublicInfo() => Ok("This is public information.");

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts(
            int pageNumber = 1, int pageSize = 10, string? nameFilter = null, int? categoryIdFilter = null,
            int? unitsInStockFilter = null, decimal? unitPriceFilter = null, [FromQuery] List<int>? productIds = null)
        {
            var result = await _productService.GetAllAsync(nameFilter, categoryIdFilter, unitsInStockFilter, unitPriceFilter, productIds, pageNumber, pageSize);
            return Ok(new ResponseModel<PaginatedResult<GetProductDto>> { Data = result, Success = true });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound(new ResponseModel<GetProductDto> { Success = false, Error = $"Product with ID {id} not found.", ErrorCode = 404 })
                                   : Ok(new ResponseModel<GetProductDto> { Data = product });
        }

        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<GetProductsIncludeDto>>> GetAllProductsWithInclude(
            int pageNumber = 1, int pageSize = 10, string? nameFilter = null, int? categoryIdFilter = null,
            int? unitsInStockFilter = null, decimal? unitPriceFilter = null, [FromQuery] List<int>? productIds = null)
        {
            var result = await _productService.GetAllIncludeAsync(nameFilter, categoryIdFilter, unitsInStockFilter, unitPriceFilter, productIds, pageNumber, pageSize);
            return Ok(new ResponseModel<PaginatedResult<GetProductsIncludeDto>> { Data = result, Success = true });
        }

        [HttpGet("include/{id}")]
        public async Task<ActionResult<GetProductsIncludeDto>> GetProductByIdWithInclude(int id)
        {
            var product = await _productService.GetByIdIncludeAsync(id);
            return product == null ? NotFound(new ResponseModel<GetProductsIncludeDto> { Success = false, Error = $"Product with ID {id} not found.", ErrorCode = 404 })
                                   : Ok(new ResponseModel<GetProductsIncludeDto> { Data = product });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto p)
        {
            if (p == null) return BadRequest(new ResponseModel<object> { Success = false, Error = "Please fill in field.", ErrorCode = 400 });
            await _productService.AddAsync(p);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product)
        {
            if (product == null) return BadRequest(new ResponseModel<object> { Success = false, Error = "Please fill in field.", ErrorCode = 400 });
            try { await _productService.UpdateAsync(product); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try { await _productService.DeleteAsync(id); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }
        }
    }
}