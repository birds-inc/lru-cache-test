using System;

namespace LRUCache.Model.Exceptions {
    public class CacheKeyNotFoundException : Exception {
        public CacheKeyNotFoundException(string keyName, Exception innerException)
            : base("Key not found: " + keyName, innerException) { }

        public CacheKeyNotFoundException(string keyName)
            : base("Key not found: " + keyName) { }

        public CacheKeyNotFoundException(byte[] keyName)
            : this(GetStringForBytes(keyName)) { }

        public CacheKeyNotFoundException(byte[] keyName, Exception innerException)
            : this(GetStringForBytes(keyName), innerException) { }

        private static string GetStringForBytes(byte[] bytes) {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
