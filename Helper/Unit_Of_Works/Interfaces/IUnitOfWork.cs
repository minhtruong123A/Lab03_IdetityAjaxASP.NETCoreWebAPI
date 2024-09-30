using BusinessObjects;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Unit_Of_Works.Interfaces
{
    public interface IUnitOfWork
    {
        public ProductRepository ProductRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public UserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}
