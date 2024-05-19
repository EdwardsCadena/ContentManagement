using Microsoft.EntityFrameworkCore;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Data;
using Proyecto.Infrastructure.Interfaces;

namespace Proyecto.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
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
            };
            _context.Users.Add(registro);
            await _context.SaveChangesAsync();
        }
    }
}
