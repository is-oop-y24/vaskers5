using System.Linq;
using System.Threading.Tasks.Sources;

namespace Isu.Services
{
    public class Student
    {
        public Student(int id, string name, Group group)
        {
            Name = name;
            Id = id;
            Group = group;
        }

        public string Name { get; }
        public int Id { get; }
        public Group Group { get; set; }
    }
}