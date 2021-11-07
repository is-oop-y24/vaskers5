using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class StudentWithTimeTable : Student
    {
        public StudentWithTimeTable(int id, string name, ExtraGroup group)
            : base(id, name, group)
        {
            Timetable = group.TimeTable;
            OgnpSubjects = new List<EducationCourse>() { };
            FacultySymbol = group.GroupName[0];
        }

        public List<EducationCourse> OgnpSubjects { get; set; }

        public List<Lesson> Timetable { get; set; }
        public char FacultySymbol { get; }
    }
}