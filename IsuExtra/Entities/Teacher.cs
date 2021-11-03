using System.Linq;
using System.Threading.Tasks.Sources;

namespace IsuExtra.Entities
{
    public class Teacher
    {
        public Teacher(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}