using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache.Model {
    class CacheEntry {
        private byte[] _key;
        private byte[] _value;

        public CacheEntry(byte[] key, byte[] value) {
            _key = (byte[])key.Clone();
            _value = (byte[])value.Clone();
        }

        public byte[] GetKey() { return _key; }
        public byte[] GetValue() { return _value; }

        public byte[] GetKeyCopy() { return (byte[])_key.Clone(); }
        public byte[] GetValueCopy() { return (byte[])_value.Clone(); }
    }
}
