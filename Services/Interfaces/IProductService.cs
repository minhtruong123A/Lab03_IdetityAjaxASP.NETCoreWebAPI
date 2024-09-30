﻿using BusinessObjects;
using BusinessObjects.Dtos.Products;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<GetProductsIncludeDto?> GetByIdIncludeAsync(int id);
        Task<PaginatedResult<GetProductsIncludeDto>> GetAllIncludeAsync(int pageNumber = 1, int pageSize = 10);
        Task<GetProductDto?> GetByIdAsync(int id);
        Task<PaginatedResult<GetProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Product> AddAsync(AddProductDto entity);
        Task UpdateAsync(UpdateProductDto entity);
        Task DeleteAsync(int id);
    }
}
