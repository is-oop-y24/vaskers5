using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using IsuExtra.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuExtraService _isuService;

        [SetUp]
        public void Setup()
        {
            var m3105 = new Group("M3105");
            var m3104 = new Group("M3104");
            var vasiliy = new Student( 311495,"vasiliy", m3104);
            var petya = new Student( 311496,"Petya", m3104);
            var studentsM3104 = new List<Student>() { vasiliy, petya };
            var firstStudent = new Student( 311497,"firstStudent", m3105);
            var SecondStudent = new Student( 311498,"SecondStudent", m3105);
            var studentsM3105 = new List<Student>() { firstStudent, SecondStudent };
            var groups1 = new List<Group>() { m3104, m3105 };
            var is1 = new Course(1, groups1);
            
            var m3205 = new Group("M3205");
            var m3204 = new Group("M3204");
            var vasya2228 = new Student( 312495,"vasya2228", m3204);
            var petya2 = new Student( 312496,"Petya",m3204);
            var studentsM3204 = new List<Student>() { vasya2228, petya2 }; ;
            var rofl11 = new Student( 312497, "rofl11", m3205);
            var rofl22 = new Student( 312498, "rofl22", m3205);
            var studentsM3205 = new List<Student>() { rofl11, rofl22 };
            var groups2 = new List<Group>() { m3204, m3205 };
            var is2 = new Course(2, groups2);
            var courses = new List<Course>() { is1, is2 };

            var itip = new MegaFaculty('m', "itip");
            var bit = new MegaFaculty('b', "bit");

            var povyish = new Teacher(0, "Повыш");
            var hlopotov = new Teacher(1, "Хлопотов");
            var suslina = new Teacher(2, "Суслина");
            
            var audition1 = new Audition(343);
            var audition2 = new Audition(347);
            var audition3 = new Audition(348);

            var kiberbez1 = new Lesson("Кибербез", DayOfWeek.Monday, LessonNumberStruct.SecondLesson, povyish, audition1, itip);
            var kiberbez2 = new Lesson("Кибербез", DayOfWeek.Monday, LessonNumberStruct.ThirdLesson, povyish, audition1, itip);
            var informationCulture1 = new Lesson("ЦК", DayOfWeek.Thursday, LessonNumberStruct.ThirdLesson, povyish, audition1, itip);
            var informationCulture2 = new Lesson("ЦК", DayOfWeek.Thursday, LessonNumberStruct.FourthLesson, povyish, audition1, itip);
            var informationCulture3 = new Lesson("ЦК", DayOfWeek.Saturday, LessonNumberStruct.FourthLesson, povyish, audition1, bit);
            var matan1 = new Lesson("Матан", DayOfWeek.Wednesday, LessonNumberStruct.SecondLesson, suslina, audition1, bit);
            var matan2 = new Lesson("Матан", DayOfWeek.Thursday, LessonNumberStruct.ThirdLesson, suslina, audition2, bit);
            var matan3 = new Lesson("Матан", DayOfWeek.Friday, LessonNumberStruct.ThirdLesson, suslina, audition3, bit);
            var matan4 = new Lesson("Матан", DayOfWeek.Sunday, LessonNumberStruct.FourthLesson, suslina, audition1, bit);
            
            var timeTable1 = new List<Lesson> {matan2, informationCulture1};
            var timeTable2 = new List<Lesson> {kiberbez1, matan1};
            var timeTable3 = new List<Lesson> {matan3, matan4};
            var timeTable4 = new List<Lesson> {informationCulture1, informationCulture2};
            var timeTable5 = new List<Lesson> {informationCulture3};
            var b3304 = new Group("B3304");
            var vasya_bit = new Student( 312410,"vasya_bit", b3304);
            var pet_bit = new Student( 312411,"petya_bit", b3304);
            var bit_students = new List<Student> {vasya_bit, pet_bit};
            var stream1 = new Stream("1", timeTable1);
            var stream2 = new Stream("2", timeTable2);
            var stream3 = new Stream("3", timeTable3);
            var stream4 = new Stream("4", timeTable4);
            var stream5 = new Stream("4", timeTable5);
            
            var course1 = new EducationCourse("Кибeрбез", itip, new List<Stream>{stream1, stream2});
            var course2 = new EducationCourse("Матан", bit, new List<Stream>{stream3, stream4, stream5});

            
            var isuservice = new IsuExtraService( new List<EducationCourse> {course1, course2}, courses);

            foreach (var students in new List<List<Student>>
                {studentsM3104, studentsM3105, studentsM3204, studentsM3205, bit_students})
            {
                foreach (var student in students)
                {
                    isuservice.AddStudent(student.Group, student.Name);
                }   
            }

            var extraM3204 = isuservice.AddGroupTimeTable(m3204, timeTable1);
            var extraM3205 = isuservice.AddGroupTimeTable(m3205, timeTable2);
            var extraM3104 = isuservice.AddGroupTimeTable(m3104, timeTable3);
            var extraM3105 = isuservice.AddGroupTimeTable(m3105, timeTable4);
            
            _isuService = isuservice;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3205 = _isuService.FindGroup("M3205");
            _isuService.AddStudent(m3205, "Sergei Papikyan");
            Assert.AreEqual(m3205.FindStudent("Sergei Papikyan").Group.GroupName, m3205.GroupName);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var m3205 = _isuService.FindGroup("M3205");
                _isuService.AddStudent(m3205, "Sergei Papikyan");
                _isuService.AddStudent(m3205, "Sergei Papikyan1");
                _isuService.AddStudent(m3205, "Sergei Papikyan2");
                _isuService.AddStudent(m3205, "Sergei Papikyan3");
                _isuService.AddStudent(m3205, "Sergei Papikya4");
                _isuService.AddStudent(m3205, "Sergei Papikya5");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = new Group("rofl mem rofl");
            });
        }
        [Test]
        public void SubscribeStudent_ToHisFaculty_EducationCourse()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                EducationCourse course = _isuService.EducationCourses[0];
                Student papikyan = _isuService.GetStudent(311492);
                StudentWithTimeTable papikyanBuzy = _isuService.SignUpStudentToCourse(papikyan, course);
            });
        }
        
        [Test]
        public void SubscribeStudent_ToTimeTableOverlapsCourse()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                EducationCourse course = _isuService.EducationCourses[0];
                Student papikyan = _isuService.GetStudent(311492);
                StudentWithTimeTable papikyanBuzy = _isuService.SignUpStudentToCourse(papikyan, course);
            });
        }
        
        [Test]
        public void SubscribeStudentToCourse()
        {
            EducationCourse course = _isuService.EducationCourses[1];
            Student papikyan = _isuService.GetStudent(311490);
            StudentWithTimeTable papikyanBuzy = _isuService.SignUpStudentToCourse(papikyan, course);
            Assert.AreEqual(papikyanBuzy.OgnpSubjects.Count, 1);
        }
        
        [Test]
        public void UnsubscribeStudentToEducationCourse()
        {
            EducationCourse course = _isuService.EducationCourses[1];
            Student papikyan = _isuService.GetStudent(311490);
            StudentWithTimeTable papikyanBuzy = _isuService.SignUpStudentToCourse(papikyan, course);
            _isuService.UnsubscribeStudentFromCourse(papikyanBuzy, course);
            Assert.AreEqual(papikyanBuzy.OgnpSubjects.Count, 0);
        }
    }
}