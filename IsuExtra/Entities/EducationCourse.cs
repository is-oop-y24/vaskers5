using System.Collections.Generic;
using System.Linq;

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
        public MegaFaculty Faculty { get; set; }
        private List<Stream> Streams { get; set; }

        public Stream AddStream(Stream stream)
        {
            Streams.Add(stream);
            return stream;
        }

        public Stream FindStudentStream(StudentWithTimeTable student)
        {
            return Streams.FirstOrDefault(stream => stream.ContainsStudent(student));
        }

        public List<Stream> GetStreams()
        {
            return Streams;
        }
    }
}