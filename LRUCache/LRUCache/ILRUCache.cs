using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache {
    public interface ILRUCache {
        byte[] Get(byte[] key);
        void Put(byte[] key, byte[] value);
        bool Contains(byte[] key);
        void Delete(byte[] key);
    }
}
