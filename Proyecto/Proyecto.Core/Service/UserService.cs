using Microsoft.AspNetCore.Http;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _userRepository.GetLoginByCredentials(userLogin);
        }

        public int GetCurrentUserId()
        {
            // Asumiendo que tienes el ID del usuario en el claim de "sub"
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("sub").Value);
        }

        public int GetCurrentUserRoleId()
        {
            // Asumiendo que tienes el ID del rol del usuario en el claim de "role"
            var roleIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("role");
            return roleIdClaim != null ? int.Parse(roleIdClaim.Value) : 0;
        }

    }
}
