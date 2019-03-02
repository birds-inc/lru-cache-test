using System;

namespace LRUCache.Model.Exceptions {
    public class CacheKeyNotFoundException : Exception {
        public CacheKeyNotFoundException(string keyName, Exception innerException)
            : base("Key not found: " + keyName, innerException) { }

        public CacheKeyNotFoundException(string keyName)
            : base("Key not found: " + keyName) { }
    }
}
