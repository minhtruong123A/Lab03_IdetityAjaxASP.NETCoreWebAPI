using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdIncludeAsync(int id);
        Task<IEnumerable<Product>> GetAllIncludeAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
    }
}
