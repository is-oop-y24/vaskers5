using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Students = students;
            StudentsList = students.AsReadOnly();
        }

        public Group(string groupName)
            : this(groupName, new List<Student>() { }) { }
        public string GroupName { get; private set; }

        public IList<Student> StudentsList { get; }
        private List<Student> Students { get; }

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
            return Students.FirstOrDefault(student => student.Name == name);
        }

        public Student FindStudent(int id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Student AddStudent(Student slave)
        {
            if (Students.Count >= StudentsPerGroupLimit)
            {
                throw new MaxSizeGroupException();
            }

            Students.Add(slave);
            return @slave;
        }

        public bool PopStudent(Student slave)
        {
            if (FindStudent(slave.Id) == null)
                throw new StudentDontExistException();
            return Students.Remove(slave);
        }
    }
}