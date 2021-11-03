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
            GroupsTimeTable = new Dictionary<Group, List<Lesson>>() { };
            StudentsTimeTable = new Dictionary<Student, List<Lesson>>() { };
        }

        public List<EducationCourse> EducationCourses { get; set; }

        private Dictionary<Group, List<Lesson>> GroupsTimeTable { get; set; }

        private Dictionary<Student, List<Lesson>> StudentsTimeTable { get; set; }

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

            foreach (Lesson lesson in streams[0].TimeTable)
            {
                StudentsTimeTable[student].Add(lesson);
            }
        }

        public void UnsubscribeStudentFromCourse(Student student, EducationCourse course)
        {
            StudentsTimeTable[student] = StudentsTimeTable[student].Where(lesson => lesson.LessonFaculty == course.Faculty).ToList();
        }

        private List<Stream> CheckGroupTimeTable(Group group, EducationCourse course)
        {
            var streams = course.Streams.Where(stream => stream.IntersectTimeTables(GroupsTimeTable[@group])).ToList();
            return streams;
        }

        private bool CheckCourseFaculty(Group group, MegaFaculty faculty)
        {
            return faculty.FacultySymbol != group.GroupName;
        }
    }
}