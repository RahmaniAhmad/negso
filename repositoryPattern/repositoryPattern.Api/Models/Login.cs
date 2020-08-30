using System.ComponentModel.DataAnnotations;

namespace repositoryPattern.Api.Models
{
    public class Login
    {
        public string Email { set; get; }

        public string Password { set; get; }
    }
}