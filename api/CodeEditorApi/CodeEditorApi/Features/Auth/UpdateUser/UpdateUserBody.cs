using CodeEditorApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.UpdateUser
{
    public class UpdateUserBody
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string OldPassword { get; set; }

        [StringLength(20)]
        [RegularExpression(RegExp.PasswordExpression, ErrorMessage = "Password does not meet criteria")]
        public string NewPassword { get; set; }
    }
}
