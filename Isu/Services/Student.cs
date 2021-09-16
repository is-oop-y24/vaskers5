using System.Linq;
using System.Threading.Tasks.Sources;

namespace Isu.Services
{
    public class Student
    {
        public Student(int id, string name, string groupName)
        {
            Name = name;
            Id = id;
            GroupName = groupName;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}