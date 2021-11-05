using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class Course
    {
        public Course(int courseNumber, List<Group> сourseGroups)
        {
            CheckCourseNumber(courseNumber);
            CourseNumber = courseNumber;
            CourseGroups = сourseGroups;
            Groups = CourseGroups.AsReadOnly();
        }

        public Course(int courseNumber)
            : this(courseNumber, new List<Group> { }) { }
        public int CourseNumber { get; }

        public IList<Group> Groups { get; }
        private List<Group> CourseGroups { get; }

        public static void CheckCourseNumber(int courseNumber)
        {
            if (courseNumber > 5 || courseNumber < 1)
                throw new InvalidCourseNameException();
        }

        public Group FindGroup(string groupNumber)
        {
            return CourseGroups.FirstOrDefault(@group => @group.GroupName == groupNumber);
        }

        public Group AddGroup(Group group)
        {
            if (FindGroup(group.GroupName) != null)
                throw new GroupAlreadyExistException();
            CourseGroups.Add(group);
            return @group;
        }

        public Group AddGroup(string groupNumber, List<Student> students)
        {
            return @AddGroup(new Group(groupNumber, students));
        }

        public Group AddGroup(string groupNumber)
        {
            return @AddGroup(new Group(groupNumber));
        }

        public Student FindStudent(string name)
        {
            return CourseGroups.FirstOrDefault(@group => @group.FindStudent(name) != null)?.FindStudent(name);
        }

        public Student FindStudent(int id)
        {
            return CourseGroups.FirstOrDefault(@group => @group.FindStudent(id) != null)?.FindStudent(id);
        }

        public bool PopStudent(Student slave)
        {
            return CourseGroups.FirstOrDefault(@group => @group.PopStudent(slave) == true) != null;
        }

        public Student AddStudent(Student slave)
        {
            return slave.Group.AddStudent(slave);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from @group in CourseGroups where @group.GroupName == groupName select @group.StudentsList).FirstOrDefault()?.ToList();
        }
    }
}