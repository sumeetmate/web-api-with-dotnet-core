using System.ComponentModel.DataAnnotations;

namespace jwt_auth.Model
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required] public string Password { get; set; } = String.Empty;
    }
}
