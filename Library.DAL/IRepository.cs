using System;
using System.Linq;

namespace Library.Data
{
    public interface IRepository<T>
    {
        T Add(T item);
        IQueryable<T> GetAllItems();
        T GetSpecificItem(Guid id);
        T Update(T item);
        T Delete(Guid id);
    }
}
