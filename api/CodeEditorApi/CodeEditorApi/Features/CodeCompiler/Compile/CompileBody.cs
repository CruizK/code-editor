using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.CodeCompiler.Compile
{
    public class CompileBody
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Language { get; set; }

    }
}
