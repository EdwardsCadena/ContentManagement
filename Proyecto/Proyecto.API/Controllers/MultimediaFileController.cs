using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Response;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Service;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MultimediaFileController : ControllerBase
    {
        private readonly IMultimediaFile _MfileRepository;
        private readonly IMapper _maper;
        private readonly IUserService _userService;
        public MultimediaFileController(IMultimediaFile MfileRepository, IMapper mapper, IUserService userService)
        {
            _MfileRepository = MfileRepository;
            _maper = mapper;
            _userService = userService;
        }
        // GET: api/<FileController>
        [HttpGet]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _MfileRepository.GetFiles();
            var filesdto = _maper.Map<IEnumerable<MultimediaFileDTOs>>(files);
            return Ok(filesdto);
        }

        // GET api/<FileController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _MfileRepository.GetFile(id);
            var files= _maper.Map<IEnumerable<MultimediaFileDTOs>>(file);
            return Ok(files);
        }

        [HttpPost("Upload")]

        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            try
            {
                var fileDto = await _MfileRepository.UploadFile(file);
                return Ok("File uploaded successfully");

            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid file.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }

        // DELETE api/<FileController>/5
        [HttpDelete("{id}")]
        [Authorize]
        /*[Authorize(Roles = "1")]*/ // Asegura que solo usuarios autenticados puedan acceder
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {

                var roleIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RoleId");

                if (roleIdClaim != null && int.TryParse(roleIdClaim.Value, out int roleId))
                {
                    // Verificar si el usuario tiene permiso de administrador (RoleId == 1)
                    if (roleId == 1)
                    {
                        // Ahora tienes el RoleId del usuario autenticado
                        // Puedes usar el userEmail y el roleId para realizar acciones específicas basadas en el usuario

                        var result = await _MfileRepository.DeleteFile(id);

                        if (result)
                        {
                            return Ok("File deleted successfully.");
                        }
                        else
                        {
                            return NotFound("File not found.");
                        }
                    }
                    else
                    {
                        // Si el usuario no es un administrador, devolver un mensaje de error de autorización
                        return StatusCode(403, "You do not have permission to delete files.");
                    }
                }
                else
                {
                    // Si no se puede obtener el RoleId, devolver un código de estado de respuesta no autorizada
                    return Unauthorized("Unauthorized access.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error.");
            }
        }
    }
    
}
