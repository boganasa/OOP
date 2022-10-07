namespace Shops.Models;

public class Product
{
    private readonly string _name;
    private string _id;

    public Product(string id, string name)
    {
        _id = id;
        _name = name;
    }

    public string GetName() => _name;
    public string GetGuid() => _id;
}