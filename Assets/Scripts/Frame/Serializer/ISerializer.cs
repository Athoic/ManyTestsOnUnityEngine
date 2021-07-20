using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Serializer
{
    public interface ISerializer
    {
        ESerializerType SerializerType { get; }

        string Serialize(object value);

        T Deserialize<T>(string value);
    }
}
