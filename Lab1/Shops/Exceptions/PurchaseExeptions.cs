using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class WrongPurchaseAmount : Exception
{
    public WrongPurchaseAmount() { }

    public WrongPurchaseAmount(string message)
        : base(message) { }

    public WrongPurchaseAmount(string message, Exception inner)
        : base(message, inner) { }

    protected WrongPurchaseAmount(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

public class WrongProduct : Exception
{
    public WrongProduct() { }

    public WrongProduct(string message)
        : base(message) { }

    public WrongProduct(string message, Exception inner)
        : base(message, inner) { }

    protected WrongProduct(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

public class WrongNumberOfProduct : Exception
{
    public WrongNumberOfProduct() { }

    public WrongNumberOfProduct(string message)
        : base(message) { }

    public WrongNumberOfProduct(string message, Exception inner)
        : base(message, inner) { }

    protected WrongNumberOfProduct(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}