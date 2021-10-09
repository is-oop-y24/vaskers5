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

        public Product AddProduct(string productName)
        {
            if (Products.ContainsKey(productName.GetHashCode()))
                throw new ProductAlreadyExistException($"{productName} is exist");

            var product = new Product(_lastProductId++, productName);
            Products.Add(product.GetHashCode(), product);
            return product;
        }

        public List<Product> AddProduct(List<string> productNames)
        {
            var products = new List<Product>() { };
            foreach (string productName in productNames)
            {
                products.Add(AddProduct(productName));
            }

            return products;
        }

        public Shop AddShop(string shopName, string place)
        {
            var shop = new Shop(_lastShopId++, shopName, place);
            ShopList.Add(shop);
            return shop;
        }

        public List<Item> AddItemsToShop(Shop shop, List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItemsToShop(shop, item);
            }

            return items;
        }

        public Item AddItemsToShop(Shop shop, Item item)
        {
            return shop.Delivery(item);
        }

        public List<Shop> FindShopsWithItem(string itemName, int number)
        {
            if (!Products.ContainsKey(itemName.GetHashCode()))
                throw new ItemDontExistException($"{itemName} is not found!");
            var shopsWithItem = ShopList.Where(shops => shops.FindItem(itemName) != null).ToList();
            return shopsWithItem.OrderBy(shop => shop.FindItem(itemName).Price).ToList();
        }

        public Shop FindCheapestShop(string itemName, int number)
        {
            return FindShopsWithItem(itemName, number)[0];
        }

        public Item CreateItem(string itemName, int price, int number)
        {
            if (!Products.ContainsKey(itemName.GetHashCode()))
                throw new ItemDontExistException($"{itemName} is not exist");
            Product product = Products[itemName.GetHashCode()];
            return new Item(product, price, number);
        }
    }
}