using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class StudentWithTimeTable
    {
        public StudentWithTimeTable(Student student, List<Lesson> timeTable)
        {
            OgnpStudent = student;
            Timetable = timeTable;
        }

        public Student OgnpStudent { get; set; }
        public List<Lesson> Timetable { get; set; }
    }
}