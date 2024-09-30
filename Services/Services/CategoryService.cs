using BusinessObjects;
using BusinessObjects.Dtos.Categories;
using BusinessObjects.Dtos.Products;
using Helper;
using Helper.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Get Category By Id
        public async Task<GetCategoryIncludeDto?> GetCategoryByIdIncludeAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdIncludeAsync(id);
            if (category == null) return null;
            return new GetCategoryIncludeDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products != null
                    ? category.Products.Select(product => new GetCategoryWithProductsDto {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitsInStock = product.UnitsInStock,
                        UnitPrice = product.UnitPrice
                    }).ToList() : new List<GetCategoryWithProductsDto>()
            };
        }

        //Get Al Include By Id
        public async Task<Category?> GetByIdIncludeAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.GetByIdIncludeAsync(id, p => p.Products);
        }


        //Get All Include
        public async Task<PaginatedResult<GetCategoryIncludeDto>> GetAllIncludeAsync(
                    string? nameFilter = null,
                    List<int>? categoryIds = null,
                    int pageNumber = 1,
                    int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var categories = await _unitOfWork.CategoryRepository.GetAllIncludeAsync(c => c.Products);

            if (!string.IsNullOrEmpty(nameFilter))
            {
                categories = categories.Where(category =>
                    category.CategoryName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }
            if (categoryIds != null && categoryIds.Any())
            {
                categories = categories.Where(category => categoryIds.Contains(category.CategoryId));
            }

            var mapCategories = categories.Select(category => new GetCategoryIncludeDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products != null
                    ? category.Products.Select(product => new GetCategoryWithProductsDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitsInStock = product.UnitsInStock,
                        UnitPrice = product.UnitPrice
                    }).ToList()
                    : new List<GetCategoryWithProductsDto>()
            }).ToList();
            var totalCategories = mapCategories.Count();
            int totalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);
            var paginatedCategories = mapCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResult<GetCategoryIncludeDto>
            {
                Items = paginatedCategories,
                TotalCount = totalCategories,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        //Get By Id
        public async Task<GetCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return new GetCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
        }

        //Get All
        public async Task<PaginatedResult<GetCategoryDto>> GetAllAsync(
                    string? nameFilter = null,
                    List<int>? categoryIds = null,
                    int pageNumber = 1,
                    int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                categories = categories.Where(category =>
                    category.CategoryName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }
            if (categoryIds != null && categoryIds.Any())
            {
                categories = categories.Where(category => categoryIds.Contains(category.CategoryId));
            }

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

            return new PaginatedResult<GetCategoryDto>
            {
                Items = paginatedCategories,
                TotalCount = totalCategories,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        //Add
        public async Task<Category> AddAsync(AddCategoryDto entity)
        {
            var categoryModel = new Category
            {
                CategoryName = entity.CategoryName
            };
            return await _unitOfWork.CategoryRepository.AddAsync(categoryModel);
        }

        //Update
        public async Task UpdateAsync(UpdateCategoryDto entity)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(entity.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {entity.CategoryId} not found.");
            if (!string.IsNullOrEmpty(entity.CategoryName))
                category.CategoryName = entity.CategoryName;
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
        }

        //Delete
        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            await _unitOfWork.CategoryRepository.DeleteAsync(id);

        }
    }
}
