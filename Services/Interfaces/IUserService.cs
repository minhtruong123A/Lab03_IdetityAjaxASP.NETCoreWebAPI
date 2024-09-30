using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> ValidateUser(string username, string password);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
