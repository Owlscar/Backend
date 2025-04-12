using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq.Expressions;

namespace Backend.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/[Controller]/EstadoApi")]
        public IActionResult EstadoApi()
        {
            ResponseGeneralDto resposeGeneralDto = new()
            {
                Respuesta = 200,
                Mensaje = "API EN EJECUCION CORRECTA"
            };
            return Ok(resposeGeneralDto);
        }

        [HttpPost]
        [Route("/api/[Controller]/InicioSesion")]
        [AllowAnonymous]
        public async Task<IActionResult> PostIniciarSesion([FromBody] RequestInicioSesionDto requestInicioSesionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.InicioSesion(requestInicioSesionDto));
        }

        [HttpPost]
        [Route("/api/[Controller]/CrearUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CrearUsuarios([FromBody] RequestUserDto requestUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseGeneralDto responseGeneralDto = await _userService.CrearUsuario(requestUserDto);

            return Ok(responseGeneralDto);
        }

        [HttpPost]
        [Route("/api/[Controller]/ObtenerUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsuarios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.ObtenerUsuarios());
        }

        [HttpPost]
        [Route("/api/[Controller]/EstadoApi")]
        public IActionResult EstadoApi2()
        {
            try
            {
                ResponseGeneralDto resposeGeneralDto = new()
                {
                    Respuesta = 200,
                    Mensaje = "API EN EJECUCION CORRECTA"
                };
                return Ok(resposeGeneralDto);
            }
            catch (Exception ex)
            {
                ResponseGeneralDto resposeGeneralDto = new()
                {
                    Respuesta = 500,
                    Mensaje = "API CON ERROR DE EJECUCION - " + ex.Message
                };
                return BadRequest(resposeGeneralDto);
            }
        }
    }
}
