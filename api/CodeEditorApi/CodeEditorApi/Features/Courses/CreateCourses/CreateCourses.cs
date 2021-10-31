using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCourses
    {
        public Task<ActionResult<Course>> ExecuteAsync(Course course);
    }
    public class CreateCourses : ICreateCourses
    {
        private readonly CodeEditorApiContext _context;

        public CreateCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Course>> ExecuteAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }
    }
}
