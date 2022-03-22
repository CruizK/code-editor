using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Auth.VerifyAccount
{
    public class VerifyAccountBody
    {

        [Required]
        public string VerifyToken { get; set; }

    }
}
