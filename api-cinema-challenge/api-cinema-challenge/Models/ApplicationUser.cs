using Microsoft.AspNetCore.Identity;
using api_cinema_challenge.UserRoles;

namespace api_cinema_challenge.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Roles Role { get; set;}
    }
}