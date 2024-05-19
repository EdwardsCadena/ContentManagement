using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Response;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Interfaces;
using Proyecto.Infrastructure.Repository;
using System.Security.Claims;

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
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
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userRepository.GetUsers();
            var usersdto = _maper.Map<IEnumerable<UserDTOs>>(users);
            return Ok(usersdto);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            var userdt = _maper.Map<UserDTOs>(user);
            return Ok(userdt);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> PutUser(int id, UserDTOs userdto)
        {
            var userup = _maper.Map<User>(userdto);
            userup.UserId = id;
            userup.Password = _passwordService.Hash(userup.Password);
            var Update = await _userRepository.UpdateUser(userup);
            var updatedto = new ApiResponse<bool>(Update);
            return Ok(updatedto);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUser(id);
            var delete = new ApiResponse<bool>(result);
            return Ok(delete);
        }
    }
}
