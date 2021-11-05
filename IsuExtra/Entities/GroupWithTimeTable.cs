using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class GroupWithTimeTable
    {
        public GroupWithTimeTable(Group group, List<Lesson> timeTable)
        {
            StudyGroup = group;
            Timetable = timeTable;
        }

        public Group StudyGroup { get; set; }
        public List<Lesson> Timetable { get; set; }
    }
}