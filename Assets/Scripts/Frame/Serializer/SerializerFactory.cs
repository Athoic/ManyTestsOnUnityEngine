using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Serializer
{
    public class SerializerFactory
    {
        public static ISerializer GetSerializer(ESerializerType serializerType)
        {
            switch (serializerType)
            {
                case ESerializerType.Json:
                    return new JsonSerializer();
                default:
                    return null;
            }
        }
    }

    public enum ESerializerType
    {
        Json,
    }
}
