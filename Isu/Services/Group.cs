using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const int StudentsPerGroupLimit = 5;
        public Group(string groupName, List<Student> students)
        {
            if (groupName[0] == 'M' & groupName[1] == '3' & groupName.Length == 5)
            {
                for (int i = 2; i < 5; i++)
                {
                    try
                    {
                        int int32 = Convert.ToInt32(groupName[i]);
                    }
                    catch (FormatException)
                    {
                        throw new InvalidGroupException();
                    }
                }
            }
            else
            {
                throw new InvalidGroupException();
            }

            GroupName = groupName;
            StudentsList = students;
        }

        // fixed
        public Group(string groupName)
            : this(groupName, new List<Student>() { }) { }

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

        // fixed
        public Student AddStudent(Student slave)
        {
            if (StudentsList.Count == StudentsPerGroupLimit)
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