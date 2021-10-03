using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourse
{
    public interface ICreateCourse
    {
        public Task<IEnumerable<Course>> ExecuteAsync(int userId);
    }
    public class CreateCourse : ICreateCourse
    {
        private readonly CodeEditorApiContext _context;

        public CreateCourse(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
