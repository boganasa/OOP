using Shops.Entities;
using Shops.Models;
using Xunit;
namespace Shops.Test;

public class ShopService
{
    [Fact]
    public void DeliveryToShop_ShopHasProduct()
    {
        var manager = new Management();
        Product apple = manager.AddProduct("Apple");
        Product milk = manager.AddProduct("Milk");
        Product iceCream = manager.AddProduct("Ice Cream");
        Product beer = manager.AddProduct("Beer");
        Builder builder = new ShopBuilder();
        var director = new Director(builder);
        var setProperties = new List<Dictionary<string, decimal>>(2);

        var newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 5,
            [milk.GetGuid()] = 2,
        };

        setProperties.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 50,
            [milk.GetGuid()] = 100,
        };
        setProperties.Insert(1, newProperties);

        ProductSet dixySet = director.Build(setProperties);
        Shop dixy = manager.AddShop("Dixy", "Aviatorov Baltiky 9", dixySet);
        var setPropertiesDelivery = new List<Dictionary<string, decimal>>();
        newProperties = new Dictionary<string, decimal>()
        {
            [iceCream.GetGuid()] = 100,
            [beer.GetGuid()] = 20,
        };
        setPropertiesDelivery.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [iceCream.GetGuid()] = 150,
            [beer.GetGuid()] = 86,
        };
        setPropertiesDelivery.Insert(1, newProperties);
        newProperties.Clear();
        ProductSet delivery = director.Build(setPropertiesDelivery);
        manager.Delivery(dixy, delivery);
        Assert.True(dixy.IsProductExist(apple));
    }

    [Fact]
    public void ShopCanChangePrice()
    {
        var manager = new Management();
        Product apple = manager.AddProduct("Apple");
        Builder builder = new ShopBuilder();
        var director = new Director(builder);
        var setProperties = new List<Dictionary<string, decimal>>(2);

        var newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 5,
        };

        setProperties.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 50,
        };
        setProperties.Insert(1, newProperties);

        ProductSet dixySet = director.Build(setProperties);
        Shop dixy = manager.AddShop("Dixy", "Aviatorov Baltiky 9", dixySet);
        var setPropertiesDelivery = new List<Dictionary<string, decimal>>();
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 1,
        };
        setPropertiesDelivery.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
           [apple.GetGuid()] = 51,
        };
        setPropertiesDelivery.Insert(1, newProperties);
        ProductSet delivery = director.Build(setPropertiesDelivery);
        manager.Delivery(dixy, delivery);
        Assert.Equal(50, dixy.GetProductSet().GetList()[1][apple.GetGuid()]);
        dixy.ChangePrice(apple, 55);
        Assert.Equal(55, dixy.GetProductSet().GetList()[1][apple.GetGuid()]);
    }

    [Fact]
    public void TheChioestShop()
    {
        var manager = new Management();
        Product apple = manager.AddProduct("Apple");
        Product milk = manager.AddProduct("Milk");
        Product iceCream = manager.AddProduct("Ice Cream");
        Product beer = manager.AddProduct("Beer");
        Builder builder = new ShopBuilder();
        var director = new Director(builder);
        var setProperties = new List<Dictionary<string, decimal>>(2);

        var newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 1,
            [milk.GetGuid()] = 1,
            [iceCream.GetGuid()] = 1,
            [beer.GetGuid()] = 1,
        };

        setProperties.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 1,
            [milk.GetGuid()] = 1,
            [iceCream.GetGuid()] = 1,
            [beer.GetGuid()] = 1,
        };
        setProperties.Insert(1, newProperties);

        ProductSet dixySet = director.Build(setProperties);
        Shop dixy = manager.AddShop("Dixy", "Aviatorov Baltiky 9", dixySet);

        setProperties = new List<Dictionary<string, decimal>>(2);

        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 1,
            [milk.GetGuid()] = 1,
            [iceCream.GetGuid()] = 1,
            [beer.GetGuid()] = 1,
        };

        setProperties.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 2,
            [milk.GetGuid()] = 1,
            [iceCream.GetGuid()] = 1,
            [beer.GetGuid()] = 1,
        };
        setProperties.Insert(1, newProperties);

        ProductSet lentaSet = director.Build(setProperties);
        Shop lenta = manager.AddShop("Lenta", "Aviatorov Baltiky 9", lentaSet);
        Assert.Equal(dixy, manager.ChipestShop(lentaSet));
    }

    [Fact]
    public void DeliveryToShop_ShopHasProduct_CustomerHaveEnoughMoneyAndCanBuyProduct()
    {
        var manager = new Management();
        Product apple = manager.AddProduct("Apple");
        Product milk = manager.AddProduct("Milk");
        Product iceCream = manager.AddProduct("Ice Cream");
        Product beer = manager.AddProduct("Beer");
        Builder builder = new ShopBuilder();
        var director = new Director(builder);
        var setProperties = new List<Dictionary<string, decimal>>(2);

        var newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 5,
            [milk.GetGuid()] = 2,
        };

        setProperties.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 50,
            [milk.GetGuid()] = 100,
        };
        setProperties.Insert(1, newProperties);

        ProductSet dixySet = director.Build(setProperties);
        Shop dixy = manager.AddShop("Dixy", "Aviatorov Baltiky 9", dixySet);
        var setPropertiesDelivery = new List<Dictionary<string, decimal>>();
        newProperties = new Dictionary<string, decimal>()
        {
            [iceCream.GetGuid()] = 100,
            [beer.GetGuid()] = 20,
        };
        setPropertiesDelivery.Insert(0, newProperties);
        newProperties = new Dictionary<string, decimal>()
        {
            [iceCream.GetGuid()] = 150,
            [beer.GetGuid()] = 86,
        };
        setPropertiesDelivery.Insert(1, newProperties);
        newProperties.Clear();
        ProductSet delivery = director.Build(setPropertiesDelivery);
        manager.Delivery(dixy, delivery);
        var ivan = new Customer("Ivan", 1000);
        Builder builderCustomer = new ShopBuilder();
        var directorCustomer = new Director(builderCustomer);
        var newPropertiesWish = new Dictionary<string, decimal>()
        {
            [apple.GetGuid()] = 5,
            [milk.GetGuid()] = 2,
        };

        setProperties.Insert(0, newPropertiesWish);
        ProductSet customerWishList = directorCustomer.Build(setProperties);
        Assert.True(ivan.Purchase(dixy, customerWishList));
    }
}