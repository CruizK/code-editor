using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public class GetCourseDetailsResponseBody
    {
        public Course CourseDetails { get; set; }

        public List<Tutorial> CourseTutorials {get; set;}
        public List<UserTutorial> userTutorialList { get; set; }
    }

   
}
