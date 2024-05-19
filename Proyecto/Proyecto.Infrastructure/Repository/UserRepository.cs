using Microsoft.EntityFrameworkCore;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Data;
using Proyecto.Infrastructure.Interfaces;
using System.Net.Sockets;

namespace Proyecto.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        DateTime currentDate = DateTime.Now;
        private readonly ContentManagementContext _context;
        protected readonly DbSet<User> _entities;
        private readonly IPasswordService _passwordService;

        public UserRepository(ContentManagementContext context, IPasswordService passwordService)
        {
            _context = context;
            _entities = context.Set<User>();
            _passwordService = passwordService;
        }

        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x =>
                (x.Email == login.Email || x.Username == login.UserName));
        }
        public async Task RegisterUser(User user)
        {
            User registro = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now,
            };
            _context.Users.Add(registro);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUser(int id)
        {
            var Users = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return Users;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var result = await GetUser(user.UserId);
            result.Username = user.Username;
            result.Email = user.Email;
            result.Password = user.Password;
            result.UpdatedAt = currentDate;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var delete = await GetUser(id);
            _context.Remove(delete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }
}
