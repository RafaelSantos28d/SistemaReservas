using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReserva.Application.Auth.Login;
using SistemReserva.Application.Auth.Register;

namespace SistemReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        public AuthController(IRegisterService registerService, ILoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _registerService.Register(request);
            return Ok(new { message = "Usuário registrado com sucesso." });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _loginService.LoginAsync(request);
            return Ok(response);

        }
    }
}
