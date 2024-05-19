using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);

    }
}
