using System;

namespace LRUCache.Model.Exceptions {
    public class CacheKeyNotFoundException : Exception {
        public CacheKeyNotFoundException(string keyName, Exception innerException)
            : base("Key not found: " + keyName, innerException) { }

        public CacheKeyNotFoundException(string keyName)
            : base("Key not found: " + keyName) { }

        public CacheKeyNotFoundException(byte[] keyName)
            : this(System.Text.Encoding.UTF8.GetString(keyName)) { }

        public CacheKeyNotFoundException(byte[] keyName, Exception innerException)
            : this(System.Text.Encoding.UTF8.GetString(keyName), innerException) { }
    }
}
