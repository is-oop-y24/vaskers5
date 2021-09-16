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
            CheckGroupName(groupName);
            GroupName = groupName;
            StudentsList = students;
        }

        public Group(string groupName)
            : this(groupName, new List<Student>() { }) { }

        public List<Student> StudentsList { get; }
        public string GroupName { get; private set; }

        public static void CheckGroupName(string groupName)
        {
            if (groupName.Length == 5 && groupName[0] == 'M' && groupName[1] == '3')
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
        }

        public Group ChangeGroupName(string newGroupName)
        {
            CheckGroupName(newGroupName);
            GroupName = newGroupName;
            return this;
        }

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
            if (StudentsList.Count >= StudentsPerGroupLimit)
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