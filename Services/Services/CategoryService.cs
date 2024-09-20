using BusinessObjects;
using Helper.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> GetByIdIncludeAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.GetByIdIncludeAsync(id, p => p.Products);
        }

        public async Task<IEnumerable<Category>> GetAllIncludeAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllIncludeAsync(p => p.Products);
        }
        public async Task<Category?> GetByIdAsync(int id) => await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        public async Task<IEnumerable<Category>> GetAllAsync() => await _unitOfWork.CategoryRepository.GetAllAsync();
        public async Task<Category> AddAsync(Category entity) => await _unitOfWork.CategoryRepository.AddAsync(entity);
        public async Task UpdateAsync(Category entity) => await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        public async Task DeleteAsync(int id) => await _unitOfWork.CategoryRepository.DeleteAsync(id);
    }
}
