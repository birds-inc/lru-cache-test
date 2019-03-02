using LRUCache.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    public class LRUCache : ILRUCache
    {
        #region class members
        private Dictionary<byte[], byte[]> _cache;
        #endregion

        #region public methods
        public LRUCache() {
            _cache = new Dictionary<byte[], byte[]>();
        }

        public byte[] Get(byte[] key) {
            try {
                return _cache[key];
            }
            catch (KeyNotFoundException e) {
                throw new CacheKeyNotFoundException(GetStringFromBytes(key), e);
            }
        }

        public void Put(byte[] key, byte[] value){
            _cache[key] = value;
        }
        
        public bool Contains(byte[] key){
            return _cache.ContainsKey(key);
        }

        public void Delete(byte[] key){
            _cache.Remove(key);
        }
        #endregion

        #region private methods
        private byte[] GetBytesFromString(string s) {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }

        private string GetStringFromBytes(byte[] b) {
            return System.Text.Encoding.UTF8.GetString(b);
        }
        #endregion
    }
}
