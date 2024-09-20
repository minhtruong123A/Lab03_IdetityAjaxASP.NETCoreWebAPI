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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> GetByIdIncludeAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdIncludeAsync(id, p => p.Category);
        }

        public async Task<IEnumerable<Product>> GetAllIncludeAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllIncludeAsync(p => p.Category);
        }
        public async Task<Product?> GetByIdAsync(int id) => await _unitOfWork.ProductRepository.GetByIdAsync(id);
        public async Task<IEnumerable<Product>> GetAllAsync() => await _unitOfWork.ProductRepository.GetAllAsync();
        public async Task<Product> AddAsync(Product entity) => await _unitOfWork.ProductRepository.AddAsync(entity);
        public async Task UpdateAsync(Product entity) => await _unitOfWork.ProductRepository.UpdateAsync(entity);
        public async Task DeleteAsync(int id) => await _unitOfWork.ProductRepository.DeleteAsync(id);

    }
}
