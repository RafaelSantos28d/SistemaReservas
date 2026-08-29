using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Auth.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(RegisterRequest request)
        {
            ApplicationUser aplicationUser = new()
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(aplicationUser, request.Password);
            if (!result.Succeeded)
            {
                var erros = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(erros);
            }
        }
    }
}
