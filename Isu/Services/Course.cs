using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class Course
    {
        public Course(int courseNumber, List<Group> groups)
        {
            CheckCourseNumber(courseNumber);
            CourseNumber = courseNumber;
            Groups = groups;
        }

        public Course(int courseNumber)
            : this(courseNumber, new List<Group> { }) { }

        public List<Group> Groups { get; set; }

        public int CourseNumber { get; set; }

        public static void CheckCourseNumber(int courseNumber)
        {
            if (courseNumber > 5 || courseNumber < 1)
                throw new InvalidCourseNameException();
        }

        public Group FindGroup(string groupNumber)
        {
            return Groups.FirstOrDefault(@group => @group.GroupName == groupNumber);
        }

        public Group AddGroup(Group group)
        {
            if (FindGroup(group.GroupName) != null)
                throw new GroupAlreadyExistException();
            Groups.Add(group);
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
            return Groups.Select(@group => @group.FindStudent(name)).FirstOrDefault(slave => slave != null);
        }

        public Student FindStudent(int id)
        {
            return Groups.Select(@group => @group.FindStudent(id)).FirstOrDefault(slave => slave != null);
        }

        public bool PopStudent(Student slave)
        {
            return Groups.Select(@group => @group.PopStudent(slave)).Any(result => result);
        }

        public Student AddStudent(Student slave)
        {
            return slave.Group.AddStudent(slave);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group.CheckGroupName(groupName);
            return (from @group in Groups where @group.GroupName == groupName select @group.StudentsList).FirstOrDefault();
        }
    }
}