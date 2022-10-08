using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class WrongShopId : Exception
{
    public WrongShopId() { }

    public WrongShopId(string message)
        : base(message) { }

    public WrongShopId(string message, Exception inner)
        : base(message, inner) { }

    protected WrongShopId(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

public class SameShop : Exception
{
    public SameShop() { }

    public SameShop(string message)
        : base(message) { }

    public SameShop(string message, Exception inner)
        : base(message, inner) { }

    protected SameShop(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}