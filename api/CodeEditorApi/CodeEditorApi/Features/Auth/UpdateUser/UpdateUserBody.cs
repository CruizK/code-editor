using CodeEditorApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Auth.UpdateUser
{
    public class UpdateUserBody
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string OldPassword { get; set; }

        [StringLength(20)]
        [RegularExpression(RegExp.PasswordExpression, ErrorMessage = "Password does not meet criteria")]
        public string NewPassword { get; set; }
    }
}
