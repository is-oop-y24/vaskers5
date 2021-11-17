using System;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string lessonName, DayOfWeek dayNumber, LessonNumberStruct lessonNumber, Teacher teacher, Audition audition, MegaFaculty lessonFaculty)
        {
            LessonName = lessonName;
            DayNumber = dayNumber;
            LessonNumber = lessonNumber;
            LessonTeacher = teacher;
            LessonAudition = audition;
            LessonFaculty = lessonFaculty;
        }

        public string LessonName { get; set; }
        public DayOfWeek DayNumber { get; set; }

        public LessonNumberStruct LessonNumber { get; set; }

        public Teacher LessonTeacher { get; set; }
        public Audition LessonAudition { get; set; }
        public object LessonFaculty { get; set; }

        public bool CheckLessonTime(Lesson second)
        {
            return this.DayNumber == second.DayNumber && this.LessonNumber == second.LessonNumber;
        }
    }
}