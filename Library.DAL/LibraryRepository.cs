using Library.Model;
using System;
using System.Linq;

namespace Library.Data
{
    public class LibraryRepository : IRepository<LibraryItem>
    {
        private readonly ItemsDB _itemList = ItemsDB.Instance;

        public LibraryItem Add(LibraryItem item)
        {
            var itemIndex = _itemList.LibraryItems.FindIndex(i => i.Title.ToLower() == item.Title.ToLower());
            if (itemIndex == -1)
            {
                _itemList.LibraryItems.Add(item);
                item.Id = Guid.NewGuid();
            }
            else
                _itemList.LibraryItems[itemIndex].Count++;
            return item;
        }

        public LibraryItem Delete(Guid id)
        {
            var item = _itemList.LibraryItems.Find(i => i.Id == id);
            if (item != null)
            {
                if (item.Count == 1)
                    _itemList.LibraryItems.Remove(item);
                else if (item.Count > 1)
                    item.Count--;
            }
            return item;
        }

        public IQueryable<LibraryItem> GetAllItems()
        {
            return _itemList.LibraryItems.AsQueryable();
        }

        public LibraryItem GetSpecificItem(Guid id)
        {
            return _itemList.LibraryItems.Find(i => i.Id == id);
        }

        /// <summary>
        /// The function gets the item that is trying to be added to the repository.
        /// </summary>
        /// <returns>The existing item. If the added item doesn't exist in the stock, the function returns null.</returns>
        public LibraryItem GetItemByExistingOne(LibraryItem newAddedItem)
        {
            var existingItem = _itemList.LibraryItems.Find(i => i.Title == newAddedItem.Title);
            return existingItem;
        }

        public LibraryItem Update(LibraryItem item)
        {
            var old = _itemList.LibraryItems.Find(i => i.Id == item.Id);
            if (old != null)
            {
                _itemList.LibraryItems.Remove(old);
                _itemList.LibraryItems.Add(item);
                item.Id = Guid.NewGuid();
            }
            return item;
        }
        public LibraryItem Update(LibraryItem oldItem, LibraryItem newItem)
        {
            if (oldItem != null)
            {
                int itemCount = oldItem.Count;
                _itemList.LibraryItems.Remove(oldItem);
                _itemList.LibraryItems.Add(newItem);
                newItem.Id = Guid.NewGuid();
                if (itemCount == newItem.Count)
                    newItem.Count = itemCount;
            }
            return newItem;
        }
    }
}
