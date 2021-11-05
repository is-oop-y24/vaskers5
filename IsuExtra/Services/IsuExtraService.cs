using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;
namespace IsuExtra.Services
{
    public class IsuExtraService : Isu.Services.IsuService
    {
        public IsuExtraService(List<EducationCourse> educationCourses, List<Course> courses)
            : base(courses)
        {
            EducationCourses = educationCourses;
            GroupsTimeTable = new List<GroupWithTimeTable>() { };
            StudentsTimeTable = new List<StudentWithTimeTable>() { };
        }

        public List<EducationCourse> EducationCourses { get; set; }

        private List<GroupWithTimeTable> GroupsTimeTable { get; set; }

        private List<StudentWithTimeTable> StudentsTimeTable { get; set; }

        public void AddNewEducationCourse(EducationCourse eduCourse)
        {
            EducationCourses.Add(eduCourse);
        }

        public void SignUpStudentToCourse(Student student, EducationCourse course)
        {
            List<Stream> streams = CheckGroupTimeTable(student.Group, course);
            if (streams == null)
            {
                throw new CourseIntersectException("This course overlaps with your group schedule");
            }
            else if (!CheckCourseFaculty(student.Group, course.Faculty))
            {
                throw new CourseFacultyException(
                    "You are trying to enroll in a course that belongs to your mega-faculty");
            }

            int systemStudentIndex = GetIndexOfStudent(student);
            foreach (Lesson lesson in streams[0].TimeTable)
            {
                StudentsTimeTable[systemStudentIndex].Timetable.Add(lesson);
            }
        }

        public void UnsubscribeStudentFromCourse(Student student, EducationCourse course)
        {
            int systemStudentIndex = GetIndexOfStudent(student);
            var newStudentTimeTable = StudentsTimeTable[systemStudentIndex].Timetable.Where(lesson => lesson.LessonFaculty != course.Faculty).ToList();
            StudentsTimeTable[systemStudentIndex].Timetable = newStudentTimeTable;
        }

        private List<Stream> CheckGroupTimeTable(Group group, EducationCourse course)
        {
            var streams = course.GetStreams().Where(stream => stream.IntersectTimeTables(GroupsTimeTable[GetIndexOfGroup(group)].Timetable)).ToList();
            return streams;
        }

        private bool CheckCourseFaculty(Group group, MegaFaculty faculty)
        {
            return faculty.FacultySymbol != group.GroupName;
        }

        private int GetIndexOfStudent(Student sampleStudent)
        {
            for (int index = 0; index < StudentsTimeTable.Count; index++)
            {
                if (StudentsTimeTable[index].OgnpStudent == sampleStudent)
                    return index;
            }

            return -1;
        }

        private int GetIndexOfGroup(Group sampleGroup)
        {
            for (int index = 0; index < StudentsTimeTable.Count; index++)
            {
                if (GroupsTimeTable[index].StudyGroup == sampleGroup)
                    return index;
            }

            return -1;
        }
    }
}