using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class ProductExeptions : Exception
{
    public ProductExeptions() { }

    public ProductExeptions(string message)
        : base(message) { }

    public ProductExeptions(string message, Exception inner)
        : base(message, inner) { }

    protected ProductExeptions(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}