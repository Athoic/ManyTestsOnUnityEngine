using Frame.Serializer;
using UnityEngine;

namespace Frame.Persistence
{
    public class PersistenceKeys
    {
        public static readonly PersistenceKey IsAutoLockOn = new PersistenceKey("IsAutoLockOn");

    }

    public class Persistence
    {
        private static ISerializer _serializer;

        public static void Write<T>(PersistenceKey key, T value)
        {
            InitSerializer();
            PlayerPrefs.SetString(key.Key, _serializer.Serialize(value));
        }

        public static T Read<T>(PersistenceKey key, T defaultValue)
        {
            string str=PlayerPrefs.GetString(key.Key);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            InitSerializer();
            return _serializer.Deserialize<T>(str);
        }

        private static void InitSerializer()
        {
            if (_serializer != null)
                return;

            _serializer = SerializerFactory.GetSerializer(ESerializerType.Json);
        }
    }

    public struct PersistenceKey
    {
        public string Key { get; set; }

        public PersistenceKey(string key)
        {
            Key = key;
        }
    }
}
