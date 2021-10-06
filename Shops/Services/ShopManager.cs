using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _lastShopId = 0;
        private int _lastProductId = 0;
        public ShopManager()
        {
            Products = new Dictionary<int, Product>() { };
            ShopList = new List<Shop>() { };
        }

        private Dictionary<int, Product> Products { get; }
        private List<Shop> ShopList { get; }

        public void AddProduct(string productName)
        {
            if (Products.ContainsKey(Hash(productName)))
                throw new ProductAlreadyExistException($"{productName} is exist");

            var product = new Product(_lastProductId++, productName);
            Products.Add(Hash(product), new Product(_lastProductId++, productName));
        }

        public void AddProduct(List<string> productNames)
        {
            foreach (string productName in productNames)
            {
                AddProduct(productName);
            }
        }

        public bool ProductExist(string productName)
        {
            return Products.ContainsKey(Hash(productName));
        }

        public Shop AddShop(string shopName, string place)
        {
            var shop = new Shop(_lastShopId++, shopName, place);
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
                throw new ItemDontExistException($"{itemName} is not found!");
            var shopsWithItem = ShopList.Where(shops => shops.FindItem(itemName) != null).ToList();
            return shopsWithItem.OrderBy(shop => shop.FindItem(itemName).Price).ToList();
        }

        public Shop FindCheapestShop(string itemName, int number)
        {
            return FindShopsWithItem(itemName, number)[0];
        }

        private static int Hash(string productName)
        {
            return productName.GetHashCode();
        }

        private static int Hash(Product product)
        {
            return Hash(product.Name);
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