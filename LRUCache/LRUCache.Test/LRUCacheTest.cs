using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LRUCache;
using LRUCache.Model.Exceptions;
using System.Linq;

namespace LRUCache.Test {
    [TestClass]
    public class LRUCacheTest {
        #region constants
        private const string TEST_KEY = "somekey";
        private const string TEST_VALUE = "somevalue";
        private const string TEST_VALUE_NEW = "somevaluenew";

        private byte[] TestKeyBytes {
            get { return System.Text.Encoding.UTF8.GetBytes(TEST_KEY); }
        }

        private byte[] TestValueBytes {
            get { return System.Text.Encoding.UTF8.GetBytes(TEST_VALUE); }
        }

        private byte[] TestValueNewBytes {
            get { return System.Text.Encoding.UTF8.GetBytes(TEST_VALUE_NEW); }
        }
        #endregion

        #region setup/teardown
        private ILRUCache _lru;

        [TestInitialize]
        public void Initialize() {
            _lru = new LRUCache();
        }
        #endregion

        #region basic tests
        [TestMethod]
        [ExpectedException(typeof(CacheKeyNotFoundException))]
        public void LRUCache_GetNonExistentKey_ThrowsCacheKeyNotFoundException() {
            _lru.Get(TestKeyBytes);
        }

        [TestMethod]
        public void LRUCache_GetJustCachedValue_ReturnsCachedValue() {
            byte[] key = TestKeyBytes;
            byte[] value = TestValueBytes;

            _lru.Put(key, value);

            byte[] returnedValue = _lru.Get(key);
            Assert.IsTrue(value.SequenceEqual(returnedValue));
        }

        [TestMethod]
        public void LRUCache_UpdatingCachedValue_ReturnedUpdatedValue() {
            byte[] key = TestKeyBytes;
            byte[] value = TestValueBytes;
            byte[] valueNew = TestValueNewBytes;

            _lru.Put(key, value);
            _lru.Put(key, valueNew);

            byte[] returnedValue = _lru.Get(key);
            Assert.IsTrue(valueNew.SequenceEqual(returnedValue));
        }

        [TestMethod]
        [ExpectedException(typeof(CacheKeyNotFoundException))]
        public void LRUCache_GetDeletedValue_ThrowsCacheKeyNotFoundException() {
            byte[] key = TestKeyBytes;
            byte[] value = TestValueBytes;

            _lru.Put(key, value);
            _lru.Delete(key);
            _lru.Get(key);
        }

        [TestMethod]
        public void LRUCache_CheckExistingValueIsContained_ReturnsTrue() {
            byte[] key = TestKeyBytes;
            byte[] value = TestValueBytes;

            _lru.Put(key, value);
            bool isContained = _lru.Contains(key);
            Assert.IsTrue(isContained);
        }

        [TestMethod]
        public void LRUCache_CheckNotExistingValueIsContained_ReturnsFalse() {
            byte[] key = TestKeyBytes;

            bool isContained = _lru.Contains(key);
            Assert.IsFalse(isContained);
        }
        #endregion

        #region least recently used tests
        [TestMethod]
        public void LRUCache_CacheHitsCapacity_AllItemsCached() {
            // ASCII encodes to one byte per character
            byte[] item1 = System.Text.Encoding.UTF8.GetBytes("a");
            byte[] item2 = System.Text.Encoding.UTF8.GetBytes("b");
            byte[] item3 = System.Text.Encoding.UTF8.GetBytes("c");
            byte[] item4 = System.Text.Encoding.UTF8.GetBytes("d");

            _lru.SetCapacity(4);
            _lru.Put(item1, item1);
            _lru.Put(item2, item2);
            _lru.Put(item3, item3);
            _lru.Put(item4, item4);

            Assert.IsTrue(_lru.Contains(item1));
            Assert.IsTrue(_lru.Contains(item2));
            Assert.IsTrue(_lru.Contains(item3));
            Assert.IsTrue(_lru.Contains(item4));
        }

        [TestMethod]
        public void LRUCache_CacheExceedsCapacity_LeastRecentlyUsedItemEvicted() {
            // ASCII encodes to one byte per character
            byte[] item1 = System.Text.Encoding.UTF8.GetBytes("a");
            byte[] item2 = System.Text.Encoding.UTF8.GetBytes("b");
            byte[] item3 = System.Text.Encoding.UTF8.GetBytes("c");
            byte[] item4 = System.Text.Encoding.UTF8.GetBytes("d");

            _lru.SetCapacity(3);
            _lru.Put(item1, item1);
            _lru.Put(item2, item2);
            _lru.Put(item3, item3);
            _lru.Put(item4, item4);

            // item 1 was least recently used, so it has been evicted from the cache
            Assert.IsFalse(_lru.Contains(item1));
            Assert.IsTrue(_lru.Contains(item2));
            Assert.IsTrue(_lru.Contains(item3));
            Assert.IsTrue(_lru.Contains(item4));
        }

        [TestMethod]
        public void LRUCache_CacheExceedsCapacityAfterItemAccessed_LeastRecentlyUsedItemEvicted() {
            // ASCII encodes to one byte per character
            byte[] item1 = System.Text.Encoding.UTF8.GetBytes("a");
            byte[] item2 = System.Text.Encoding.UTF8.GetBytes("b");
            byte[] item3 = System.Text.Encoding.UTF8.GetBytes("c");
            byte[] item4 = System.Text.Encoding.UTF8.GetBytes("d");

            _lru.SetCapacity(3);
            _lru.Put(item1, item1);
            _lru.Put(item2, item2);
            _lru.Put(item3, item3);
            _lru.Get(item1);
            _lru.Put(item4, item4);

            // item 2 was least recently used, so it has been evicted from the cache
            Assert.IsTrue(_lru.Contains(item1));
            Assert.IsFalse(_lru.Contains(item2));
            Assert.IsTrue(_lru.Contains(item3));
            Assert.IsTrue(_lru.Contains(item4));
        }
        #endregion
    }
}
