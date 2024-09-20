using BusinessObjects;
using Helper.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductStoreContext _storeContext;

        public UnitOfWork(ProductStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        private ProductRepository productRepository;
        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_storeContext);
                }

                return productRepository;
            }
        }

        private CategoryRepository categoryRepository;
        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(_storeContext);
                }

                return categoryRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _storeContext.SaveChangesAsync();
        }
    }
}
