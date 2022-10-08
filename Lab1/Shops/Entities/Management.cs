using Shops.Exceptions;
using Shops.Models;
namespace Shops.Entities;

public class Management
{
    private Dictionary<string, Product> _listOfProducts;
    private Dictionary<string, Shop> _listOfShops;

    public Management()
    {
        _listOfProducts = new Dictionary<string, Product>();
        _listOfShops = new Dictionary<string, Shop>();
    }

    public Management(Dictionary<string, Product> listOfProducts, Dictionary<string, Shop> listOfShops)
    {
        _listOfProducts = listOfProducts;
        _listOfShops = listOfShops;
    }

    public Dictionary<string, Shop> GetShops() => _listOfShops;
    public Dictionary<string, Product> GetProducts() => _listOfProducts;
    public Shop AddShop(string name, string address, ProductSet productSet)
    {
        if (_listOfShops.Any(list => list.Value.GetName() == name && list.Value.GetAddress() == address))
        {
            throw new SameShop($"Shop {name} local at {address} is already exist");
        }

        var id = Guid.NewGuid();
        var newShop = new Shop(id.ToString(), name, address, productSet);
        _listOfShops[id.ToString()] = newShop;
        return newShop;
    }

    public Product AddProduct(string name)
    {
        if (_listOfProducts.Any(list => list.Value.GetName() == name))
        {
            throw new ProductExeptions($"Product {name} is already exist");
        }

        var id = Guid.NewGuid();
        var newProduct = new Product(id.ToString(), name);
        _listOfProducts[id.ToString()] = newProduct;
        return newProduct;
    }

    public void Delivery(Shop shop, ProductSet productSet)
    {
        string id = shop.GetGuid();
        if (!_listOfShops.ContainsKey(shop.GetGuid()))
        {
            throw new WrongShopId($"Shop {shop.GetName()} with ID {shop.GetGuid()} doesn't exist");
        }

        Dictionary<string, decimal> number = productSet.GetList()[0];
        Dictionary<string, decimal> price = productSet.GetList()[1];
        Dictionary<string, decimal> realNumber = _listOfShops[id].GetProductSet().GetList()[0];
        Dictionary<string, decimal> realPrice = _listOfShops[id].GetProductSet().GetList()[1];
        Dictionary<string, decimal> newNumber = realNumber;
        Dictionary<string, decimal> newPrice = realPrice;
        foreach (KeyValuePair<string, decimal> i in number)
        {
            if (realNumber.ContainsKey(i.Key))
            {
                newNumber[i.Key] = realNumber[i.Key] + number[i.Key];
            }
            else
            {
                newNumber[i.Key] = number[i.Key];
                newPrice[i.Key] = price[i.Key];
            }
        }

        Builder builder = new ShopBuilder();
        var director = new Director(builder);
        var setProperties = new List<Dictionary<string, decimal>>();
        setProperties.Add(newNumber);
        setProperties.Add(newPrice);
        ProductSet newSet = director.Build(setProperties);
        shop.SetProductSet(newSet);
    }

    public Shop GetShopList(string id) => _listOfShops[id];

    public Shop ChipestShop(ProductSet productSet)
    {
        const decimal key = -1;
        decimal minSum = key;
        decimal sum = key;
        decimal wallet = 1000000;
        var shopWithMinSum = new Shop();
        var ivan = new Customer("Ivan", wallet);
        foreach (Shop shop in _listOfShops.Select(curShop => curShop.Value))
        {
            try
            {
                sum = ivan.Receipt(shop, productSet);
            }
            catch
            {
                // ignored
            }

            if (sum != 0 && (sum < minSum || minSum == key))
            {
                minSum = sum;
                shopWithMinSum = shop;
            }
        }

        if (minSum == key)
        {
            throw new WrongNumberOfProduct($"There are no shop, where all products in");
        }

        return shopWithMinSum;
    }
}