using LRUCache.Model;
using LRUCache.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache {
    public class LRUCache : ILRUCache {
        /// <summary>
        /// Implements a last-recently-used cache
        /// </summary>
        
        #region constants
        // default capacity is 1GB
        const int DEFAULT_CAPACITY = 1024 * 1024 * 1024;
        #endregion

        #region class members
        /// <summary>
        /// stores a key-value hash map of cached items
        /// </summary>
        private Dictionary<byte[], CacheItem> _cacheDictionary;

        /// <summary>
        /// stores a doubly-linked list of cached items
        /// </summary>
        private CacheList _cacheList;
        
        /// <summary>
        /// total capacity of cache in bytes
        /// </summary>
        private int _capacity;
        #endregion

        #region constructors
        /// <summary>
        /// Create a new Least-Recently-Used cache
        /// </summary>
        /// <param name="capacity"></param>
        public LRUCache(int capacity) {
            _cacheDictionary = new Dictionary<byte[], CacheItem>(new ByteArrayComparer());
            _cacheList = new CacheList();
            SetCapacity(capacity);
        }

        /// <summary>
        /// Create a new Least-Recently-Used cache
        /// </summary>
        public LRUCache() : this(DEFAULT_CAPACITY) { }
        #endregion

        #region public methods
        /// <summary>
        /// Sets a new total capacity, in bytes, for the cache.
        /// Will evict cached items as necessary.
        /// </summary>
        public void SetCapacity(int capacityInBytes) {
            EnsureCapacity(capacityInBytes);
            _capacity = capacityInBytes;
        }

        /// <summary>
        /// Gets the value for a given key in the cache.
        /// Throws a CacheKeyNotFoundException if key is not present in cache
        /// Getting an item from the cache will update it as most-recently-used.
        /// </summary>
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

        /// <summary>
        /// Inserts or updates an item in the cache.
        /// Putting an item in the cache will update it as most-recently-used.
        /// </summary>
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

        /// <summary>
        /// Returns true if the specified key is cached.
        /// Does not update most-recently-used status for that key.
        /// </summary>
        public bool Contains(byte[] key) {
            return _cacheDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
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
