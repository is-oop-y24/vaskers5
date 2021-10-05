using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, float money)
        {
            if (money < 0)
                throw new ShopsException("Money in Person cannot be < 0");
            Name = name;
            Money = money;
        }

        public string Name { get; }

        public float Money { get; set; }
    }
}