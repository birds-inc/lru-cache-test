using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache {
    /// <summary>
    /// Defines the basic interface used to interact with the cache
    /// </summary>
    public interface ILRUCache {
        void SetCapacity(UInt64 numberOfBytes);

        byte[] Get(byte[] key);
        void Put(byte[] key, byte[] value);
        bool Contains(byte[] key);
        void Delete(byte[] key);
    }
}
