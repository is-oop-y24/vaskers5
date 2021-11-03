using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Entities
{
    public class Stream
    {
        public Stream(string streamName, List<Lesson> timeTable)
        {
            StreamName = streamName;
            TimeTable = timeTable;
        }

        public string StreamName { get; set; }

        public List<Lesson> TimeTable { get; set; }

        public bool IntersectTimeTables(List<Lesson> otherTimeTable)
        {
            return this.TimeTable.All(lesson => !TimeTable.Any(otherLesson => otherLesson.CheckLessonTime(lesson)));
        }
    }
}