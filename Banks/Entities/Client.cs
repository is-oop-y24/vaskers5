using System.Collections.Generic;

namespace Banks.Entities
{
    public class Client
    {
        public Client(string name, bool notifications, string address = default, int passportId = default)
        {
            Name = name;
            Notifications = notifications;
            Address = address;
            PassportId = passportId;
            if (address == default || passportId == default)
                IsFishy = true;
            else IsFishy = false;
            NotificationsHistory = new List<string>() { };
        }

        public bool IsFishy { get; set; }
        public string Name { get; set; }
        public bool Notifications { get; set; }
        public string Address { get; set; }
        public int PassportId { get; set; }
        public List<string> NotificationsHistory { get; set; }
    }
}