using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    private string _name;
    private decimal _wallet;

    public Customer(string name, decimal wallet)
    {
        _name = name;
        _wallet = wallet;
    }

    public decimal GetWallet() => _wallet;

    public void Pay(decimal price)
    {
        if (price > _wallet)
        {
            throw new WrongPurchaseAmount($"You haven't dot enough money: {_wallet} < {price}");
        }

        _wallet -= price;
    }

    public decimal Receipt(Shop shop, ProductSet shoppingList)
    {
        ProductSet shopSet = shop.GetProductSet();
        decimal sum = 0;
        foreach (KeyValuePair<string, decimal> i in shoppingList.GetList()[0])
        {
            if (!shopSet.GetList()[0].ContainsKey(i.Key))
            {
                throw new WrongProduct($"Product with ID {i.Key} doesn't exist");
            }

            if (i.Value > shopSet.GetList()[0][i.Key])
            {
                throw new WrongNumberOfProduct($"There are not enough products with ID {i.Key} : {i.Value} > {shopSet.GetList()[0][i.Key]} ");
            }

            sum += shopSet.GetList()[1][i.Key] * i.Value;
        }

        return sum;
    }

    public bool Purchase(Shop shop, ProductSet shoppingList)
    {
        ProductSet shopSet = shop.GetProductSet();
        decimal sum = Receipt(shop, shoppingList);
        if (sum > _wallet)
        {
            throw new WrongPurchaseAmount($"Customer {_name} hasn't got enough money: {_wallet} < {sum}");
        }
        else
        {
            Pay(sum);
            foreach (KeyValuePair<string, decimal> i in shoppingList.GetList()[0])
            {
                shopSet.GetList()[0][i.Key] -= i.Value;
            }

            shop.SetProductSet(shopSet);
        }

        return true;
    }
}