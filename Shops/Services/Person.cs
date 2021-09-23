namespace Shops.Services
{
    public class Person
    {
        public Person(string name, float money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; }

        public float Money { get; set; }
    }
}