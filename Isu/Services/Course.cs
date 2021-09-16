using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Isu.Services
{
    public class Course
    {
        public Course(int courseNumber, List<Group> groups)
        {
            CourseNumber = courseNumber;
            Groups = groups;
        }

        public Course(int courseNumber)
        {
            CourseNumber = courseNumber;
            Groups = new List<Group>() { };
        }

        public List<Group> Groups { get; set; }

        public int CourseNumber { get; set; }
        public Group FindGroup(string groupNumber)
        {
            return Groups.FirstOrDefault(@group => @group.GroupName == groupNumber);
        }

        public Group AddGroup(Group group)
        {
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

        // fixed
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
            return FindGroup(slave.GroupName).AddStudent(slave);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from @group in Groups where @group.GroupName == groupName select @group.StudentsList).FirstOrDefault();
        }
    }
}