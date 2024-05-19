using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Response;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Interfaces;

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        private readonly IPasswordService _passwordService;
        public UserController(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _maper = mapper;
            _passwordService = passwordService;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserDTOs userDTOs)
        {
            // Aquí mapeo el objeto userDTOs (Data Transfer Object) a una entidad User
            var user = _maper.Map<User>(userDTOs);
            // Aquí hasheo la contraseña del usuario para que no se almacene en texto plano
            user.Password = _passwordService.Hash(user.Password);
            // Aquí se registra el usuario (se guarda en la base de datos) llamando al método RegisterUser del repositorio
            await _userRepository.RegisterUser(user);
            // Aquí se prepara la respuesta. Se envuelve el DTO en un objeto ApiResponse
            var response = new ApiResponse<UserDTOs>(userDTOs);
            // Aquí se devuelve la respuesta con un código de estado HTTP 200 (OK)
            return Ok(response);
        }
    }
}
