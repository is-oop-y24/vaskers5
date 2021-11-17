using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Entities
{
    public class Stream
    {
        public Stream(string streamName, List<Lesson> timeTable)
        {
            StreamName = streamName;
            TimeTable = timeTable;
            StreamStudents = new List<StudentWithTimeTable>() { };
        }

        public string StreamName { get; set; }

        public List<Lesson> TimeTable { get; set; }
        private List<StudentWithTimeTable> StreamStudents { get; set; }

        public StudentWithTimeTable AddStudent(StudentWithTimeTable student)
        {
            StreamStudents.Add(student);
            return student;
        }

        public Lesson AddLesson(Lesson lesson)
        {
            TimeTable.Add(lesson);
            return lesson;
        }

        public StudentWithTimeTable RemoveStudent(StudentWithTimeTable student)
        {
            StreamStudents.Remove(student);
            return student;
        }

        public bool ContainsStudent(StudentWithTimeTable student)
        {
            return StreamStudents.Contains(student);
        }

        public IList<StudentWithTimeTable> GetStudents()
        {
            return StreamStudents.AsReadOnly();
        }

        public bool IntersectTimeTables(List<Lesson> otherTimeTable)
        {
            return this.TimeTable.All(lesson => TimeTable.Any(otherLesson => otherLesson.CheckLessonTime(lesson)));
        }
    }
}