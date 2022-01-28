using CodeEditorApiDataAccess.StaticData;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Auth
{
    public class RoleRequestBody
    {
        [Required]
        public Roles Role { get; set; }
    }
}
