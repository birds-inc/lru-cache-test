using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache.Model {
    public class CacheItem {
        #region private members
        private byte[] _key;
        private byte[] _value;
        private ByteArrayComparer _comparer;
        #endregion

        #region public properties
        public CacheItem NextItem { get; set; }
        public CacheItem PrevItem { get; set; }
        #endregion

        #region public methods
        public CacheItem(byte[] key, byte[] value) {
            _key = (byte[])key.Clone();
            _value = (byte[])value.Clone();
            _comparer = new ByteArrayComparer();
        }

        public byte[] GetKey() { return _key; }
        public byte[] GetValue() { return _value; }

        public byte[] GetKeyCopy() { return (byte[])_key.Clone(); }
        public byte[] GetValueCopy() { return (byte[])_value.Clone(); }

        public int GetSize() { return _value.Length; }
        #endregion public method

        #region equality
        public override bool Equals(object obj) {
            if (!(obj is CacheItem)) {
                return false;
            }
            return _comparer.Equals(this.GetKey(), (obj as CacheItem).GetKey());
        }

        public override int GetHashCode() {
            return _comparer.GetHashCode(this.GetKey());
        }
        #endregion
    }
}
