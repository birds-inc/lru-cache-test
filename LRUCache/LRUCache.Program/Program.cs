using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache.Program {
    class Program {
        static void Main(string[] args) {
            LRUCache cache = new LRUCache();

            Console.WriteLine("Testing LRU cache to determine optimal capacity to remain under 1 GB memory usage");

            UInt64 targetUsage = 1024 * 1024 * 1024;
            byte b = 0;

            while (targetUsage > GetMemoryUsage(cache)) {
                cache.Put(new byte[] { b }, new byte[] { b });
                b++;
            }

            Console.WriteLine("Set capacity to " + cache.GetUsedCapacity().ToString() + " to keep memory usage under 1 GB");
        }

        private static UInt64 GetMemoryUsage(LRUCache cache) {
            // fun hack, taken from https://stackoverflow.com/questions/605621/how-to-get-object-size-in-memory

            UInt64 size = 0;
            using (Stream s = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, cache);
                size = (UInt64)s.Length;
            }
            return size;
        }
    }
}
