using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private string _guid;
    private string _name;
    private string _address;
    private ProductSet _productSet;

    public Shop()
    {
        _guid = string.Empty;
        _name = string.Empty;
        _address = string.Empty;
        _productSet = new ProductSet();
    }

    public Shop(string id, string name, string address, ProductSet productSet)
    {
        _guid = id;
        _name = name;
        _address = address;
        _productSet = productSet;
    }

    public string GetGuid() => _guid;
    public string GetName() => _name;
    public string GetAddress() => _address;
    public ProductSet GetProductSet() => _productSet;

    public void SetProductSet(ProductSet set) => _productSet = set;

    public void ChangePrice(Product product, decimal newPrice)
    {
        string id = product.GetGuid();
        if (!IsProductExist(product))
        {
            throw new WrongProduct($"Product {product.GetName()} doesn't exist");
        }

        _productSet.GetList()[1][id] = newPrice;
    }

    public bool IsProductExist(Product product)
    {
        return _productSet.GetList()[0].Keys.Any(prod => prod == product.GetGuid());
    }
}