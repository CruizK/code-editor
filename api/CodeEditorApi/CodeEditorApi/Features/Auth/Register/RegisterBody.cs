using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Auth.Register
{
    public class RegisterBody
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
