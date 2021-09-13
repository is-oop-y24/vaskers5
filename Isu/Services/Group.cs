using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        public Group(string groupName, List<Student> students)
        {
            if (groupName[0] != 'M' || groupName[1] != '3')
            {
                throw new InvalidGroupException();
            }

            GroupName = groupName;
            StudentsList = students;
        }

        public Group(string groupName)
        {
            if (groupName[0] != 'M' || groupName[1] != '3')
            {
                throw new InvalidGroupException();
            }

            GroupName = groupName;
            StudentsList = new List<Student>() { };
        }

        public List<Student> StudentsList { get; set; }
        public string GroupName { get; set; }
        public Student FindStudent(string name)
        {
            return StudentsList.FirstOrDefault(student => student.Name == name);
        }

        public Student FindStudent(int id)
        {
            return StudentsList.FirstOrDefault(student => student.Id == id);
        }

        public Student AddStudent(Student slave)
        {
            if (StudentsList.Count == 5)
            {
                throw new MaxSizeGroupException();
            }

            StudentsList.Add(slave);
            return @slave;
        }

        public bool PopStudent(Student slave)
        {
            if (FindStudent(slave.Id) == null)
                throw new StudentDontExistException();
            return StudentsList.Remove(slave);
        }
    }
}