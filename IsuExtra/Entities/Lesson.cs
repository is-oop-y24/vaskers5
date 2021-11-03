using System;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private int _dayNumber;
        private int _lessonNumber;
        private Lesson(string lessonName, int dayNumber, int lessonNumber, Group lessonGroup, Teacher teacher, Audition audition, MegaFaculty lessonFaculty)
        {
            LessonName = lessonName;
            DayNumber = dayNumber;
            LessonNumber = lessonNumber;
            LessonGroup = lessonGroup;
            LessonTeacher = teacher;
            LessonAudition = audition;
            LessonFaculty = lessonFaculty;
        }

        public string LessonName { get; set; }
        public int DayNumber
        {
            get => _dayNumber;
            set
            {
                if (value is < 1 or > 7)
                    throw new InvalidDayException("Day must be in 1..7 range");
                _dayNumber = value;
            }
        }

        public int LessonNumber
        {
            get => _lessonNumber;
            set
            {
                if (value is < 1 or > 9)
                    throw new InvalidDayException("Day must be in 1..8 range");
                _lessonNumber = value;
            }
        }

        public Group LessonGroup { get; set; }
        public Teacher LessonTeacher { get; set; }
        public Audition LessonAudition { get; set; }
        public object LessonFaculty { get; set; }

        public bool CheckLessonTime(Lesson second)
        {
            return this.DayNumber == second.DayNumber && this.LessonNumber == second.LessonNumber;
        }
    }
}