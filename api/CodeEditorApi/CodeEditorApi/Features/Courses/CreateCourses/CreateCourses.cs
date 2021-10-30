using CodeEditorApiDataAccess.Data;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCourses
    {
        public Task<Course> ExecuteAsync(int userId, Course course);
    }
    public class CreateCourses : ICreateCourses
    {
        private readonly CodeEditorApiContext _context;

        public CreateCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<Course> ExecuteAsync(int userId, Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }
    }
}
