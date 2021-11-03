using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class EducationCourse
    {
        public EducationCourse(string educationCourseName, MegaFaculty faculty, List<Stream> streams)
        {
            EducationCourseName = educationCourseName;
            Streams = streams;
            Faculty = faculty;
        }

        public string EducationCourseName { get; set; }
        public List<Stream> Streams { get; set; }
        public MegaFaculty Faculty { get; set; }
    }
}