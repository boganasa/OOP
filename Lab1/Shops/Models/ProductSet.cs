namespace Shops.Models;

public class ProductSet
{
    private List<Dictionary<string, decimal>> _setProperties = new List<Dictionary<string, decimal>>();

    public void Add(Dictionary<string, decimal> setProperty)
    {
        _setProperties.Add(setProperty);
    }

    public List<Dictionary<string, decimal>> GetList() => _setProperties;
}

public abstract class Builder
{
    public abstract void BuildPrice(Dictionary<string, decimal> mySet);
    public abstract void BuildNumber(Dictionary<string, decimal> mySet);
    public abstract ProductSet GetSet();
}

public class CustomerBuilder : Builder
{
    private ProductSet _productSet = new ProductSet();
    public override void BuildNumber(Dictionary<string, decimal> mySet) => _productSet.Add(mySet);

    public override void BuildPrice(Dictionary<string, decimal> mySet) { }
    public override ProductSet GetSet() => _productSet;
}

public class ShopBuilder : Builder
{
    private ProductSet _productSet = new ProductSet();
    public override void BuildNumber(Dictionary<string, decimal> mySet) => _productSet.Add(mySet);

    public override void BuildPrice(Dictionary<string, decimal> mySet) => _productSet.Add(mySet);
    public override ProductSet GetSet() => _productSet;
}

public class Director
{
    private Builder _builder;

    public Director(Builder builder)
    {
        _builder = builder;
    }

    public ProductSet Build(List<Dictionary<string, decimal>> setProperties)
    {
        _builder.BuildNumber(setProperties[0]);
        _builder.BuildPrice(setProperties[1]);
        return _builder.GetSet();
    }
}