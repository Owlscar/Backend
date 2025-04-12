using Backend.Dtos;
using Backend.Repositories;
using Backend.Utilities;

namespace Backend.Services
{
    public class UserService
    {
        private readonly JwtSettingsDto _jwtSettings;
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository, JwtSettingsDto jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<ResponseGeneralDto> CrearUsuario(RequestUserDto requestUserDto)
        {
            ResponseGeneralDto responseGeneralDto = new();

            requestUserDto.Password = Encrypt.GetSHA256(requestUserDto.Password);
            var result = await _userRepository.CrearUsuario(requestUserDto);
            if (result == 1)
            {
                responseGeneralDto.Respuesta = 1;
                responseGeneralDto.Mensaje = "Ususario creado exitosamente.";
            }
            else
            {
                responseGeneralDto.Respuesta = 0;
                responseGeneralDto.Mensaje = "Algo pasó.";
            }

            return responseGeneralDto;
        }

        public async Task<List<ResponseUserDto>> ObtenerUsuarios()
        {
            return await _userRepository.ObtenerUsuarios();
        }

        public async Task<ResponseInicionSesionDto> InicioSesion(RequestInicioSesionDto requestInicioSesionDto)
        {
            ResponseInicionSesionDto responseInicionSesionDto = new();
            requestInicioSesionDto.Contrasena = Encrypt.GetSHA256(requestInicioSesionDto.Contrasena);
            var userResult = await _userRepository.Login(requestInicioSesionDto);

            if (userResult)
            {
                responseInicionSesionDto = JwtUtility.GenTokenkey(responseInicionSesionDto, _jwtSettings);
                responseInicionSesionDto.Respuesta = 1;
                responseInicionSesionDto.Mensaje = "Inicio de Sesion Exitoso. Bienvenid@ Usuario ";
            }
            else
            {
                responseInicionSesionDto.Respuesta = 0;
                responseInicionSesionDto.Mensaje = "Inicio de Sesion Fallido, Usuario y/o Contraeña estan incorrectos.";
            }

            return responseInicionSesionDto;
        }
    }
}
