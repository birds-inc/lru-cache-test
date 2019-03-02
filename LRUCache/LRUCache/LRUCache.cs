using LRUCache.Model;
using LRUCache.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    public class LRUCache : ILRUCache {
        #region class members
        private Dictionary<byte[], CacheEntry> _cache;
        #endregion

        #region public methods
        public LRUCache() {
            _cache = new Dictionary<byte[], CacheEntry>(new KeyComparator());
        }

        public void SetCapacity(int numberOfBytes) {
        }

        public byte[] Get(byte[] key) {
            try {
                return _cache[key].GetValueCopy();
            }
            catch (KeyNotFoundException e) {
                throw new CacheKeyNotFoundException(key, e);
            }
        }

        public void Put(byte[] key, byte[] value) {
            var entry = new CacheEntry(key, value);
            _cache[entry.GetKey()] = entry;
        }

        public bool Contains(byte[] key) {
            return _cache.ContainsKey(key);
        }

        public void Delete(byte[] key) {
            _cache.Remove(key);
        }
        #endregion
    }
}
