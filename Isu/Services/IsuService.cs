using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Isu.Tools;

namespace Isu.Services
{
     public class IsuService : IIsuService
     {
         private int _lastStudentId = 311490;
         public IsuService(List<Course> courses)
         {
             Courses = courses;
         }

         private List<Course> Courses { get; }
         public Course AddCourse(Course course)
         {
             if (Courses.Any(elem => elem.CourseNumber == course.CourseNumber))
             {
                 throw new CourseAlreadyExistException();
             }

             Courses.Add(course);
             return course;
         }

         public Course AddCourse(int courseNumber)
         {
             return AddCourse(new Course(courseNumber));
         }

         public Course AddCourse(int courseNumber, List<Group> groups)
         {
             return AddCourse(new Course(courseNumber, groups));
         }

         public Course FindCourse(int courseNumber)
         {
             return Courses.FirstOrDefault(course => course.CourseNumber == courseNumber);
         }

         public Group AddGroup(string name)
         {
             Group.CheckGroupName(name);
             int courseNumber = name[2];
             Course.CheckCourseNumber(courseNumber);
             Course course = FindCourse(courseNumber);
             return course != null ? course.AddGroup(name) : AddCourse(new Course(courseNumber)).AddGroup(name);
         }

         public Student AddStudent(Group group, string name)
         {
             return group.AddStudent(new Student(_lastStudentId++, name, group));
         }

         public Student GetStudent(int id)
         {
             Student student = Courses.Select(course => course.FindStudent(id)).FirstOrDefault(student => student != null);
             if (student == null)
                 throw new StudentDontExistException();
             return student;
         }

         public Student FindStudent(string name)
         {
             return Courses.Select(course => course.FindStudent(name)).FirstOrDefault(student => student != null);
         }

         public List<Student> FindStudents(string groupName)
         {
             return Courses.Select(course => course.FindStudents(groupName)).FirstOrDefault(group => group != null);
         }

         public List<Student> FindStudents(int courseNumber)
         {
             Course.CheckCourseNumber(courseNumber);
             Course course = Courses.Find(course => course.CourseNumber == courseNumber);
             if (course == null)
                 throw new CourseDontExistException();
             return course.Groups.SelectMany(gr => gr.StudentsList).ToList();
         }

         public Group FindGroup(string groupName)
         {
             Group.CheckGroupName(groupName);
             return Courses.Select(course => course.FindGroup(groupName)).FirstOrDefault(group => group != null);
         }

         public List<Group> FindGroups(int courseNumber)
         {
             Course.CheckCourseNumber(courseNumber);
             return FindCourse(courseNumber).Groups;
         }

         public void ChangeStudentGroup(Student student, Group newGroup)
         {
             student.Group.PopStudent(student);
             student.Group = newGroup;
             newGroup.AddStudent(student);
         }
     }
}