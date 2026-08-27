using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Reserva> Reservas { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
