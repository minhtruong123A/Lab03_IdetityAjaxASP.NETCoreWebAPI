using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Helper;
using Helper.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProductsIncludeDto?> GetByIdIncludeAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdIncludeAsync(id, p => p.Category);
            if (product == null) return null;
            return new GetProductsIncludeDto
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
        }

        //Get All Include
        public async Task<PaginatedResult<GetProductsIncludeDto>> GetAllIncludeAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var products = await _unitOfWork.ProductRepository.GetAllIncludeAsync(p => p.Category);
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

            return new PaginatedResult<GetProductsIncludeDto>
            {
                Items = paginatedProducts,
                TotalCount = totalProducts,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        //Get By Id
        public async Task<GetProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return null;
            return new GetProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
            };
        }
        
        //Get All
        public async Task<PaginatedResult<GetProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var products = await _unitOfWork.ProductRepository.GetAllAsync();
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

            return new PaginatedResult<GetProductDto>
            {
                Items = paginatedProducts,
                TotalCount = totalProducts,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        
        //Add
        public async Task<Product> AddAsync(AddProductDto entity)
        {
            var product = new Product
            {
                ProductName = entity.ProductName,
                CategoryId = entity.CategoryId,
                UnitsInStock = entity.UnitsInStock,
                UnitPrice = entity.UnitPrice
            };
            return await _unitOfWork.ProductRepository.AddAsync(product);
        }
        
        //Update
        public async Task UpdateAsync(UpdateProductDto entity)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(entity.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {entity.ProductId} not found.");
            if (!string.IsNullOrEmpty(entity.ProductName))
                product.ProductName = entity.ProductName;
            if (entity.CategoryId.HasValue && entity.CategoryId > 0)
                product.CategoryId = entity.CategoryId.Value;
            if (entity.UnitsInStock >= 0)
                product.UnitsInStock = entity.UnitsInStock;
            if (entity.UnitPrice >= 0)
                product.UnitPrice = entity.UnitPrice;
            await _unitOfWork.ProductRepository.UpdateAsync(product);
        }
        
        //Delete
        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            await _unitOfWork.ProductRepository.DeleteAsync(id);
        }

    }
}
