using System;
using System.Collections.Generic;
using Shops.Entities;
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
            Items = new Dictionary<string, Item>() { };
        }

        public string Name { get; }

        public string Place { get; }

        public int Id { get; }
        public Dictionary<string, Item> Items { get; private set; }

        public Item Delivery(Item item)
        {
            string hash = item.SystemProduct.Name;
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
                throw new ShopDontContainsItemException($"{itemName} is not found");
            if (item.Price * number > person.Money)
                throw new NotEnoughMoneyException($"Price is {item.Price * number}, your money is {person.Money}");
            if (item.Number < number)
            {
                throw new ShopDontContainsItemException($"Shop does not contains so much of {itemName}," +
                                                        $" current number is {item.Number}");
            }

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
            return Items.ContainsKey(itemName) ? Items[itemName] : null;
        }

        public void ChangePrice(string itemName, float newPrice)
        {
            Item item = FindItem(itemName);
            if (item != null)
                throw new ShopDontContainsItemException($"{itemName} is not found,");
            FindItem(itemName).Price = newPrice;
        }
    }
}