using System.Web.Script.Serialization;

namespace Frame.Serializer
{
    public class JsonSerializer : ISerializer
    {
        public ESerializerType SerializerType { get; } = ESerializerType.Json;
        private JavaScriptSerializer _javaScriptSerializer;

        public T Deserialize<T>(string value)
        {
            return (T)_javaScriptSerializer.Deserialize(value, typeof(T));
        }

        public string Serialize(object value)
        {
            return _javaScriptSerializer.Serialize(value);
        }

        public JsonSerializer()
        {
            _javaScriptSerializer = new JavaScriptSerializer();
        }
    }
}
