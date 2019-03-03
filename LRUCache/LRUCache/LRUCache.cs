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
        #region constants
        // default capacity is 1GB
        const int DEFAULT_CAPACITY = 1024 * 1024 * 1024;
        #endregion

        #region class members
        private Dictionary<byte[], CacheItem> _cacheDictionary;
        private CacheList _cacheList;
        private int _capacity;
        #endregion

        #region constructors
        public LRUCache(int capacity) {
            _cacheDictionary = new Dictionary<byte[], CacheItem>(new ByteArrayComparer());
            _cacheList = new CacheList();
            SetCapacity(capacity);
        }

        public LRUCache() : this(DEFAULT_CAPACITY) { }
        #endregion

        #region public methods
        public void SetCapacity(int numberOfBytes) {
            _capacity = numberOfBytes;
        }

        public byte[] Get(byte[] key) {
            try {
                var item = _cacheDictionary[key];
                _cacheList.RemoveItem(item);
                _cacheList.InsertItemAtHead(item);
                return item.GetValueCopy();
            }
            catch (KeyNotFoundException e) {
                throw new CacheKeyNotFoundException(key, e);
            }
        }

        public void Put(byte[] key, byte[] value) {
            var item = new CacheItem(key, value);

            if (Contains(key)) {
                var origItem = _cacheDictionary[key];
                _cacheList.RemoveItem(origItem);
            }

            int targetCapacity = _capacity - item.GetSize();
            EnsureCapacity(targetCapacity);
            _cacheList.InsertItemAtHead(item);

            _cacheDictionary[item.GetKey()] = item;
        }

        public bool Contains(byte[] key) {
            return _cacheDictionary.ContainsKey(key);
        }

        public void Delete(byte[] key) {
            var item = _cacheDictionary[key];
            _cacheDictionary.Remove(key);
            _cacheList.RemoveItem(item);
        }
        #endregion

        #region private methods
        private void EnsureCapacity(int targetCapacity) {
            while (_cacheList.TotalSize > targetCapacity) {
                var item = _cacheList.RemoveLastItem();
                _cacheDictionary.Remove(item.GetKey());
            }
        }
        #endregion
    }
}
