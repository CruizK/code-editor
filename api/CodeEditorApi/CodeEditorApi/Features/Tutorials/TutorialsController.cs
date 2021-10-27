using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialsController : ControllerBase
    {
        private readonly IGetTutorialsCommand _getTutorialsCommand;
        public TutorialsController(IGetTutorialsCommand getTutorialsCommand)
        {
            _getTutorialsCommand = getTutorialsCommand;
        }

        /// <summary>
        /// Gets a tutorial created by a User by Tutorial Id
        /// </summary>
        [HttpGet("GetUserTutorials")]
        [Authorize]
        public async Task<Tutorial> GetUserTutorials([FromQuery] int TutorialId)
        {
            var tutorial = new GetTutorialsBody{
                TutorialId = TutorialId
            };

            return await _getTutorialsCommand.ExecuteAsync(tutorial);
        }

        
    }
}
