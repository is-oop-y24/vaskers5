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
         public IsuService(List<Course> isuCourses)
         {
             IsuCourses = isuCourses;
             Courses = IsuCourses.AsReadOnly();
         }

         public IList<Course> Courses { get; }
         private List<Course> IsuCourses { get; }
         public Course AddCourse(Course course)
         {
             if (IsuCourses.Any(elem => elem.CourseNumber == course.CourseNumber))
             {
                 throw new CourseAlreadyExistException();
             }

             IsuCourses.Add(course);
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
             return IsuCourses.FirstOrDefault(course => course.CourseNumber == courseNumber);
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
             Student student = IsuCourses.FirstOrDefault(course => course.FindStudent(id) != null)?.FindStudent(id);
             if (student == null)
                 throw new StudentDontExistException();
             return student;
         }

         public Student FindStudent(string name)
         {
             return IsuCourses.FirstOrDefault(course => course.FindStudent(name) != null)?.FindStudent(name);
         }

         public List<Student> FindStudents(string groupName)
         {
             return IsuCourses.FirstOrDefault(course => course.FindGroup(groupName) != null)?.FindGroup(groupName).StudentsList.ToList();
         }

         public List<Student> FindStudents(int courseNumber)
         {
             Course.CheckCourseNumber(courseNumber);
             Course course = IsuCourses.Find(course => course.CourseNumber == courseNumber);
             if (course == null)
                 throw new CourseDontExistException();
             return course.Groups.SelectMany(gr => gr.StudentsList).ToList();
         }

         public Group FindGroup(string groupName)
         {
             Group.CheckGroupName(groupName);
             return IsuCourses.FirstOrDefault(course => course.FindGroup(groupName) != null)?.FindGroup(groupName);
         }

         public List<Group> FindGroups(int courseNumber)
         {
             Course.CheckCourseNumber(courseNumber);
             return FindCourse(courseNumber).Groups.ToList();
         }

         public void ChangeStudentGroup(Student student, Group newGroup)
         {
             student.ChangeStudentGroup(newGroup);
         }
     }
}