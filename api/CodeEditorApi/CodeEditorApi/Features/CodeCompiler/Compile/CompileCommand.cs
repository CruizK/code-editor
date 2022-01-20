using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.CodeCompiler.Compile
{
    public interface ICompileCommand
    {
        public Task<ActionResult<string>> ExecuteAsync(CompileBody compileBody);
    }

    public class CompileCommand : ICompileCommand
    {

        public CompileCommand()
        {

        }

        public Task<ActionResult<string>> ExecuteAsync(CompileBody compileBody)
        {
            throw new NotImplementedException();
        }
    }
}
