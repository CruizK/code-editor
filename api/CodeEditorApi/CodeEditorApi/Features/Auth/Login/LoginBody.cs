using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Auth.Login
{
    public class LoginBody
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
