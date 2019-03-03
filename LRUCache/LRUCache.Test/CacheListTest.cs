using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LRUCache;
using LRUCache.Model.Exceptions;
using System.Linq;
using LRUCache.Model;

namespace LRUCache.Test {
    [TestClass]
    public class CacheListTest {
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
        private CacheList _cacheList;

        [TestInitialize]
        public void Initialize() {
            _cacheList = new CacheList();
        }
        #endregion

        #region basic tests
        #endregion
    }
}
