using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoryIncludeDto?> GetCategoryByIdIncludeAsync(int id);
        Task<Category?> GetByIdIncludeAsync(int id);
        Task<PaginatedResult<GetCategoryIncludeDto>> GetAllIncludeAsync(
                    string? nameFilter = null,
                    List<int>? categoryIds = null,
                    int pageNumber = 1,
                    int pageSize = 10);
        Task<GetCategoryDto?> GetByIdAsync(int id);
        Task<PaginatedResult<GetCategoryDto>> GetAllAsync(
                    string? nameFilter = null,
                    List<int>? categoryIds = null,
                    int pageNumber = 1,
                    int pageSize = 10);
        Task<Category> AddAsync(AddCategoryDto entity);
        Task UpdateAsync(UpdateCategoryDto entity);
        Task DeleteAsync(int id);
    }
}
