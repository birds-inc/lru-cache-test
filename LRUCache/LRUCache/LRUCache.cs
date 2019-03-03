using LRUCache.Model;
using LRUCache.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache {
    [Serializable]
    public class LRUCache : ILRUCache {
        /// <summary>
        /// Implements a last-recently-used cache
        /// </summary>
        
        #region constants
        // default capacity is 1GB
        // you can use the included LRUCache.Program to determine a good target value for your machine
        const UInt64 DEFAULT_CAPACITY = 1024 * 1024 * 1024;
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
        #endregion

        #region public properties 
        /// <summary>
        /// total capacity of cache in bytes
        /// </summary>
        public UInt64 Capacity { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Create a new Least-Recently-Used cache
        /// </summary>
        /// <param name="capacity"></param>
        public LRUCache(UInt64 capacity) {
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
        public void SetCapacity(UInt64 capacityInBytes) {
            EnsureCapacity(capacityInBytes);
            Capacity = capacityInBytes;
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

            UInt64 targetCapacity = Capacity - item.GetSize();
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

        /// <summary>
        /// Get total capacity consumed
        /// </summary>
        public UInt64 GetUsedCapacity() {
            return _cacheList.TotalSize;
        }
        #endregion

        #region private methods
        private void EnsureCapacity(UInt64 targetCapacity) {
            while (_cacheList.TotalSize > targetCapacity) {
                var item = _cacheList.RemoveLastItem();
                _cacheDictionary.Remove(item.GetKey());
            }
        }
        #endregion
    }
}
