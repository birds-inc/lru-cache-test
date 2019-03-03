using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache.Model {
    [Serializable]
    class ByteArrayComparer : EqualityComparer<byte[]> {
        public override bool Equals(byte[] bytes1, byte[] bytes2) {
            return bytes1.SequenceEqual(bytes2);
        }

        public override int GetHashCode(byte[] bytes) {
            unchecked {
                // 17 and 31 are traditional prime factors, to ensure even distribution of hashes
                int hash = 17;
                foreach (byte b in bytes) {
                    hash = hash * 31 + b;
                }
                return hash;
            }
        }
    }
}
