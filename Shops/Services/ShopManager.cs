using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _lastId = 0;
        public ShopManager()
        {
            Products = new Dictionary<int, string>() { };
            ShopList = new List<Shop>() { };
        }

        public Dictionary<int, string> Products { private get; set; }
        private List<Shop> ShopList { get; }

        public void AddProduct(string productName)
        {
            Products.Add(Hash(productName), productName);
        }

        public void AddProduct(List<string> productNames)
        {
            foreach (string productName in productNames)
            {
                Products.Add(Hash(productName), productName);
            }
        }

        public bool ProductExist(string productName)
        {
            return Products.ContainsValue(productName);
        }

        public Shop AddShop(string shopName, string place)
        {
            var shop = new Shop(_lastId++, shopName, place);
            ShopList.Add(shop);
            return shop;
        }

        public Item AddItemsToShop(Shop shop, Item item)
        {
            return ProductExist(item.Name) ? shop.Delivery(item) : null;
        }

        public List<Item> AddItemsToShop(Shop shop, List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItemsToShop(shop, item);
            }

            return items;
        }

        public Item AddItemsToShop(Shop shop, string itemName, int price, int number)
        {
            Item item = CreateItem(itemName, price, number);
            return item != null ? AddItemsToShop(shop, CreateItem(itemName, price, number)) : null;
        }

        public List<Item> AddItemsToShop(Shop shop, List<string> itemNames, List<int> prices, List<int> numbersOfItems)
        {
            return AddItemsToShop(shop, CreateListItems(itemNames, prices, numbersOfItems));
        }

        public List<Shop> FindShopsWithItem(string itemName, int number)
        {
            if (!ProductExist(itemName))
                throw new ItemDontExistException();
            var shopsWithItem = ShopList.Where(shops => shops.FindItem(itemName) != null).ToList();
            return shopsWithItem.OrderBy(shop => shop.FindItem(itemName).Price).ToList();
        }

        public Shop FindCheapestShop(string itemName, int number)
        {
            return FindShopsWithItem(itemName, number)[0];
        }

        internal static int Hash(string name)
        {
            return name.GetHashCode();
        }

        private Item CreateItem(string itemName, int price, int number)
        {
            return ProductExist(itemName) ? new Item(itemName, price, number) : null;
        }

        private List<Item> CreateListItems(List<string> itemNames, List<int> prices, List<int> numbers)
        {
            var listItems = new List<Item>() { };
            for (int i = 0; i < itemNames.Count(); i++)
            {
                Item item = CreateItem(itemNames[i], prices[i], numbers[i]);
                if (item != null)
                    listItems.Add(item);
            }

            return listItems;
        }
    }
}