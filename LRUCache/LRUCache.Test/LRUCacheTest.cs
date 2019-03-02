using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LRUCache;
using LRUCache.Model.Exceptions;

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

        #region tests
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
            Assert.AreEqual(value, returnedValue);
        }

        [TestMethod]
        public void LRUCache_UpdatingCachedValue_ReturnedUpdatedValue() {
            byte[] key = TestKeyBytes;
            byte[] value = TestValueBytes;
            byte[] valueNew = TestValueNewBytes;

            _lru.Put(key, value);
            _lru.Put(key, valueNew);

            byte[] returnedValue = _lru.Get(key);
            Assert.AreEqual(valueNew, returnedValue);
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
    }
}
