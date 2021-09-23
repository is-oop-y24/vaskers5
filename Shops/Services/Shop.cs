using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        public Shop(int id, string name, string place)
        {
            Id = id;
            Name = name;
            Place = place;
            Items = new Dictionary<int, Item>() { };
        }

        public string Name { get; }

        public string Place { get; }

        public int Id { get; }
        public Dictionary<int, Item> Items { get; private set; }

        public Item Delivery(Item item)
        {
            int hash = Hash(item);
            if (!Items.ContainsKey(hash))
            {
                Items.Add(hash, item);
            }
            else
            {
                Items[hash].Number += item.Number;
            }

            return item;
        }

        public void Sell(string itemName, int number, Person person)
        {
            Item item = FindItem(itemName);

            if (item == null)
                throw new ShopDontContainsItemException();
            else if (item.Price * number > person.Money)
                throw new NotEnoughMoneyException();
            else if (item.Number < number)
                throw new ShopDontContainsItemException();

            item.Number -= number;
            person.Money -= number * item.Price;
        }

        public Item FindItem(string itemName, int number)
        {
            Item item = FindItem(itemName);
            if (item == null)
                return null;
            return item.Number < number ? null : item;
        }

        public Item FindItem(string itemName)
        {
            try
            {
                return Items[ShopManager.Hash(itemName)];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public void ChangePrice(string itemName, float newPrice)
        {
            FindItem(itemName).Price = newPrice;
        }

        private int Hash(Item item)
        {
            return ShopManager.Hash(item.Name);
        }
    }
}