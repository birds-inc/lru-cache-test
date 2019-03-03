using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache.Model {
    [Serializable]
    public class CacheList {
        #region private members
        private CacheItem _head;
        private CacheItem _tail;
        #endregion

        #region public properties
        public UInt64 TotalSize { get; private set; }
        #endregion

        #region constructor
        public CacheList() {
            _head = null;
            _tail = null;
            TotalSize = 0;
        }
        #endregion

        #region public methods
        public void InsertItemAtHead(CacheItem item) {
            // link new item to previous head
            item.NextItem = _head;
            if (_head != null) {
                _head.PrevItem = item;
            }

            // set new item as new head
            _head = item;

            // if this is the only item in the list, set it to tail also
            if (item.NextItem == null) {
                _tail = item;
            }

            // update size
            TotalSize += item.GetSize();
        }

        public void RemoveItem(CacheItem item) {
            // link previous item to next item
            if (item.PrevItem != null) {
                item.PrevItem.NextItem = item.NextItem;
            }
            if (item.NextItem != null) {
                item.NextItem.PrevItem = item.PrevItem;
            }

            // if this item was the head, set the next item to be new head
            if (item == _head) {
                _head = item.NextItem;
            }

            // if this item was the tail, set the previous item to be the new tail
            if (item == _tail) {
                _tail = item.PrevItem;
            }

            // update size
            TotalSize -= item.GetSize();
        }

        public CacheItem RemoveLastItem() {
            if (_tail != null) {
                var item = _tail;
                RemoveItem(item);
                return item;
            }
            return null;
        }
        #endregion
    }
}
