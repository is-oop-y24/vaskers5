using System;
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
            GroupsTimeTable = new List<ExtraGroup>() { };
            StudentsTimeTable = new List<StudentWithTimeTable>() { };
        }

        public List<EducationCourse> EducationCourses { get; set; }

        private List<ExtraGroup> GroupsTimeTable { get; set; }

        private List<StudentWithTimeTable> StudentsTimeTable { get; set; }

        public void AddNewEducationCourse(EducationCourse eduCourse)
        {
            EducationCourses.Add(eduCourse);
        }

        public StudentWithTimeTable SignUpStudentToCourse(StudentWithTimeTable student, EducationCourse course)
        {
            List<Stream> streams = CheckGroupTimeTable(student.Group, course);
            if (streams.Count == 0)
            {
                throw new CourseIntersectException("This course overlaps with your group schedule");
            }
            else if (!CheckCourseFaculty(student.Group, course.Faculty))
            {
                throw new CourseFacultyException(
                    "You are trying to enroll in a course that belongs to your mega-faculty");
            }

            Stream stream = streams[0];
            int systemStudentIndex = StudentsTimeTable.IndexOf(student);
            StudentsTimeTable[systemStudentIndex].Timetable.AddRange(stream.TimeTable);

            stream.AddStudent(student);
            student.OgnpSubjects.Add(course);
            return stream.AddStudent(student);
        }

        public StudentWithTimeTable SignUpStudentToCourse(Student student, EducationCourse course)
        {
            return SignUpStudentToCourse(GetStudentWithTimeTable(student), course);
        }

        public void UnsubscribeStudentFromCourse(StudentWithTimeTable student, EducationCourse course)
        {
            int systemStudentIndex = StudentsTimeTable.IndexOf(student);
            var newStudentTimeTable = StudentsTimeTable[systemStudentIndex].Timetable.Where(lesson => lesson.LessonFaculty != course.Faculty).ToList();
            course.FindStudentStream(student).RemoveStudent(student);
            student.OgnpSubjects.Remove(course);
            StudentsTimeTable[systemStudentIndex].Timetable = newStudentTimeTable;
        }

        public IList<StudentWithTimeTable> GetStudents(Stream stream)
        {
            return stream.GetStudents();
        }

        public ExtraGroup AddGroupTimeTable(Group group, List<Lesson> timeTable)
        {
            var extraGroup = new ExtraGroup(group.GroupName, group.StudentsList.ToList(), timeTable);
            AddGroupWithTimeTable(extraGroup);
            var studentsWithTimeTable = group.StudentsList.Select(student =>
                new StudentWithTimeTable(student.Id, student.Name, extraGroup));
            StudentsTimeTable.AddRange(studentsWithTimeTable);
            return extraGroup;
        }

        public List<Student> GetUnsignedStudents(ExtraGroup group)
        {
            return group.StudentsList.Where(student =>
                StudentsTimeTable[GetIndexOfStudent(student)].OgnpSubjects.Count < 2).ToList();
        }

        public StudentWithTimeTable GetStudentWithTimeTable(Student student)
        {
            return StudentsTimeTable[GetIndexOfStudent(student)];
        }

        private ExtraGroup AddGroupWithTimeTable(ExtraGroup group)
        {
            GroupsTimeTable.Add(group);
            return group;
        }

        private List<Stream> CheckGroupTimeTable(Group group, EducationCourse course)
        {
            return course.GetStreams()
                .Where(stream => stream.IntersectTimeTables(GroupsTimeTable[GetIndexOfGroup(group)].TimeTable))
                .ToList();
        }

        private bool CheckCourseFaculty(Group group, MegaFaculty faculty)
        {
            var facSymbol = faculty.FacultySymbol;
            var groupFac = group.GroupName[0];
            return char.ToLower(facSymbol) != char.ToLower(groupFac);
        }

        private int GetIndexOfStudent(Student sampleStudent)
        {
            for (int index = 0; index < StudentsTimeTable.Count; index++)
            {
                if (StudentsTimeTable[index].Id == sampleStudent.Id)
                    return index;
            }

            return -1;
        }

        private int GetIndexOfGroup(Group sampleGroup)
        {
            for (int index = 0; index < StudentsTimeTable.Count; index++)
            {
                if (GroupsTimeTable[index].GroupName == sampleGroup.GroupName)
                    return index;
            }

            return -1;
        }
    }
}