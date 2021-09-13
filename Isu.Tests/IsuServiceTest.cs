using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            var vasya228 = new Student("vasya228", 311495, "M3104");
            var petya = new Student("petya228", 311496, "M3104");
            var m3104_students = new List<Student>() { vasya228, petya };
            var m3104 = new Group("M3104", m3104_students);
            var rofl1 = new Student("rofl1", 311497, "M3105");
            var rofl2 = new Student("rofl2", 311498, "M3105");
            var m3105_students = new List<Student>() { rofl1, rofl2 };
            var m3105 = new Group("M3105", m3105_students);
            var groups1 = new List<Group>() { m3104, m3105 };
            var is1 = new Course(1, groups1);
            
            var vasya2228 = new Student("vasya2228", 312495, "M3204");
            var petya2 = new Student("petya228", 312496, "M3204");
            var m3204_students = new List<Student>() { vasya2228, petya2 };
            var m3204 = new Group("M3204", m3204_students);
            var rofl11 = new Student("rofl11", 312497, "M3205");
            var rofl22 = new Student("rofl22", 312498, "M3205");
            var m3205_students = new List<Student>() { rofl11, rofl22 };
            var m3205 = new Group("M3205", m3205_students);
            var groups2 = new List<Group>() { m3204, m3205 };
            var is2 = new Course(2, groups2);
            var courses = new List<Course>() { is1, is2 };
            var isuservice = new IsuService(courses);
            
            _isuService = isuservice;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3205 = _isuService.FindGroup("M3205");
            _isuService.AddStudent(m3205, "Sergei Papikyan");
            Assert.AreEqual(m3205.FindStudent("Sergei Papikyan").GroupName, m3205.GroupName);
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
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group m3105 = _isuService.FindGroup("M3105");
                Student papikyan =_isuService.AddStudent(m3105, "Papikyan");
                Group m3204 = _isuService.FindGroup("M3204");
                _isuService.ChangeStudentGroup(papikyan, m3204);
                m3204.PopStudent(papikyan);
                m3204.PopStudent(papikyan);
                Assert.Contains(m3105.StudentsList, new[] {papikyan});
                Assert.Equals(_isuService.FindStudent("Papikyan").GroupName, "m3204");
            });
        }
    }
}