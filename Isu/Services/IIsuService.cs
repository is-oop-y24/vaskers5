using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Isu.Tools;

namespace Isu.Services
{
     public interface IIsuService
     {
         Group AddGroup(string name);
         Student AddStudent(Group @group, string name);

         Student GetStudent(int id);
         Student FindStudent(string name);
         List<Student> FindStudents(string groupName);
         List<Student> FindStudents(int courseNumber);

         Group FindGroup(string groupName);
         List<Group> FindGroups(int courseNumber);

         void ChangeStudentGroup(Student student, Group newGroup);
     }

     public class IsuService : IIsuService
     {
         public IsuService(List<Course> courses)
         {
             Courses = courses;
         }

         public List<Course> Courses { get; set; }
         public Course AddCourse(Course course)
         {
             Courses.Add(course);
             return @course;
         }

         public Course AddCourse(int courseNumber)
         {
             return @AddCourse(new Course(courseNumber));
         }

         public Course AddCourse(int courseNumber, List<Group> groups)
         {
             return @AddCourse(new Course(courseNumber, groups));
         }

         public Course FindCourse(int courseNumber)
         {
             return Courses.FirstOrDefault(course => course.CourseNumber == courseNumber);
         }

         public Group AddGroup(string name)
         {
             int courseNumber = name[2];
             Course course = FindCourse(courseNumber);
             return course != null ? @course.AddGroup(name) : @AddCourse(new Course(courseNumber)).AddGroup(name);
         }

         public Student AddStudent(Group @group, string name)
         {
             int courseNumber = @group.GroupName[2];
             return @group.AddStudent(new Student(name, GetId(name), group.GroupName));
         }

         public Student GetStudent(int id)
         {
             return Courses.Select(course => course.FindStudent(id)).FirstOrDefault(student => student != null);
         }

         public Student FindStudent(string name)
         {
             return Courses.Select(course => course.FindStudent(name)).FirstOrDefault(student => student != null);
         }

         public List<Student> FindStudents(string groupName)
         {
             return Courses.Select(course => course.FindStudents(groupName)).FirstOrDefault(@group => @group != null);
         }

         public List<Student> FindStudents(int courseNumber)
         {
             var students = new List<Student>() { };
             foreach (Group @group in Courses.Where(course => course.CourseNumber == courseNumber).SelectMany(course => course.Groups))
             {
                 students.AddRange(@group.StudentsList);
             }

             return students;
         }

         public Group FindGroup(string groupName)
         {
             return Courses.Select(course => course.FindGroup(groupName)).FirstOrDefault(@group => @group != null);
         }

         public List<Group> FindGroups(int courseNumber)
         {
             return FindCourse(courseNumber).Groups;
         }

         public void ChangeStudentGroup(Student student, Group newGroup)
         {
             student.GroupName = newGroup.GroupName;
             newGroup.AddStudent(student);
         }

         private static int GetId(string name)
         {
             int hash = 0;
             foreach (char elem in name)
             {
                 int value = elem;
                 hash += value;
             }

             return hash;

             // return name.Cast<int>().Sum();
         }
     }
}