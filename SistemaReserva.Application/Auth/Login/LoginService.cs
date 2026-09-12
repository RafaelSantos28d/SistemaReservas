using Microsoft.AspNetCore.Identity;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Auth.Login
{
    public class LoginService :ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Credenciais inválidas.");

            var senhaValida = await _userManager.CheckPasswordAsync(usuario, request.Password);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var roles = await _userManager.GetRolesAsync(usuario);
            var token = _tokenService.GerarToken(usuario, roles);
            var refreshToken = _tokenService.GerarRefreshToken();

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(usuario);

            return new LoginResponse { AccessToken = token, RefreshToken = refreshToken };
        }
    }
}
