using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024_09_21/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Sua luon CategoryController


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var products = await _productService.GetAllAsync();
            var mapProducts = products.Select(product => new GetProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
            }).ToList();
            var totalProducts = mapProducts.Count();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var paginatedProducts = mapProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationMetadata = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalProducts = totalProducts
            };

            return Ok(new
            {
                Products = paginatedProducts,
                Pagination = paginationMetadata
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            var mapProducts = new GetProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
            };

            return Ok(mapProducts);
        }

        [HttpGet("Include")]
        public async Task<ActionResult<IEnumerable<GetProductsIncludeDto>>> GetAllProductsWithInclude(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var products = await _productService.GetAllIncludeAsync();
            var mapProducts = products.Select(product => new GetProductsIncludeDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                Category = product.Category != null ? new GetCategoryDto
                {
                    CategoryId = product.Category.CategoryId,
                    CategoryName = product.Category.CategoryName
                } : null
            }).ToList();
            var totalProducts = mapProducts.Count();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var paginatedProducts = mapProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationMetadata = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalProducts = totalProducts
            };

            return Ok(new
            {
                Products = paginatedProducts,
                Pagination = paginationMetadata
            });
        }

        [HttpGet("Include/{id}")]
        public async Task<ActionResult<GetProductsIncludeDto>> GetProductByIdWithInclude(int id)
        {
            var product = await _productService.GetByIdIncludeAsync(id);

            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            var mapProducts = new GetProductsIncludeDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                Category = product.Category != null ? new GetCategoryDto
                {
                    CategoryId = product.Category.CategoryId,
                    CategoryName = product.Category.CategoryName
                } : null
            };

            return Ok(mapProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto p)
        {
            if (p == null)
                return BadRequest(new { Message = $"Please fill in field." });

            var product = new Product
            {
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                UnitsInStock = p.UnitsInStock,
                UnitPrice = p.UnitPrice
            };

            await _productService.AddAsync(product);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product)
        {
            if (product == null)
                return BadRequest(new { Message = $"Please fill in field." });

            var p = await _productService.GetByIdAsync(product.ProductId);
            if (p == null)
                return NotFound(new { Message = $"Product with ID {product.ProductId} not found." });

            if (!string.IsNullOrEmpty(product.ProductName))
                p.ProductName = product.ProductName;
            if (product.CategoryId.HasValue && product.CategoryId > 0)
                p.CategoryId = product.CategoryId.Value;
            if (product.UnitsInStock > 0)
                p.UnitsInStock = product.UnitsInStock;
            if (product.UnitPrice > 0)
                p.UnitPrice = product.UnitPrice;

            await _productService.UpdateAsync(p);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
