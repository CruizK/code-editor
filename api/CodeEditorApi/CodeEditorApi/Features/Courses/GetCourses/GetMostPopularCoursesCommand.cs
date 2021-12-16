using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetMostPopularCoursesCommand
    {
        public Task<ActionResult<List<int>>> ExecuteAsync();
    }
    public class GetMostPopularCoursesCommand : IGetMostPopularCoursesCommand
    {
        private readonly IGetCourses _getCourses;
        public GetMostPopularCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<int>>> ExecuteAsync()
        {
            return await _getCourses.GetMostPopularCourses();
        }
    }
}
