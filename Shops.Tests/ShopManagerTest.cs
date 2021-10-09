using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void SetUp()
        {
            var shopManager = new ShopManager();
            Shop magnitShop = shopManager.AddShop("Магнит", "Заречная 17");
            var itemsNames = new List<string>() {"Гречка", "Сахар", "Котлетки", "Нагетсы"};
            var itemNumbers = new List<int>() {39, 40, 59, 60};
            var itemPrices = new List<int>() {10, 20, 30, 40};
            List<Product> products = shopManager.AddProduct(itemsNames);
            var items = new List<Item>(){};
            var lens = new List<int> {products.Count, itemsNames.Count, itemNumbers.Count, itemPrices.Count};
            for (int i = 0; i < lens.Min(); i++)
                items.Add(shopManager.CreateItem(itemsNames[i], itemNumbers[i], itemPrices[i]));
            Shop lentaShop = shopManager.AddShop("Лента", "Улица Михалиа Дудина 8");
            shopManager.AddItemsToShop(lentaShop, items);
            shopManager.AddItemsToShop(magnitShop, items);
            _shopManager = shopManager;
        }

        [Test]
        public void Sale_ToPerson_WithMoney()
        {
            var buyerWithMoney = new Person("Илья", 39999);
            float moneyBefore = buyerWithMoney.Money;
            var shop = _shopManager.FindCheapestShop("Сахар", 8);
            int numberOfItemBefore = shop.FindItem("Сахар").Number;
            shop.Sell("Сахар", 8, buyerWithMoney);
            Assert.IsTrue(buyerWithMoney.Money < moneyBefore);
            Assert.AreEqual(numberOfItemBefore, shop.FindItem("Сахар").Number + 8);
        }

        [Test]
        public void Sale_ToPerson_WithoutMoney_Exception()
        {
            ShopsException shopsException = Assert.Catch<ShopsException>(() =>
            {
                var buyerWithoutMoney = new Person("Иван", 30);
                var shop = _shopManager.FindCheapestShop("Сахар", 8);
                shop.Sell("Сахар", 4, buyerWithoutMoney);
            } );
        }
        
        [Test]
        public void Find_NotExistItem_Exception()
        {
            ShopsException shopsException = Assert.Catch<ShopsException>(() =>
            {
                var buyerWithoutMoney = new Person("Иван", 3000);
                var shop = _shopManager.FindCheapestShop("Морковь", 8);
            } );
        }
        
        [Test]
        public void Sale_ToMuchItem_Exception()
        {
            ShopsException shopsException = Assert.Catch<ShopsException>(() =>
            {
                var buyer = new Person("Иван", 3000);
                var shop = _shopManager.FindCheapestShop("Котлетки", 0);
                shop.Sell("Котлетки", 900, buyer);
            } );
        }
        
        [Test]
        public void Sale_DontExistItemInShop_Exception()
        {
            ShopsException shopsException = Assert.Catch<ShopsException>(() =>
            {
                var buyer = new Person("Иван", 3000);
                var shop = _shopManager.AddShop("Пятерочка", "Заречная 18");
                shop.Sell("Пирожок с картошкой", 1, buyer);
            } );
        }
    }
        
        
}