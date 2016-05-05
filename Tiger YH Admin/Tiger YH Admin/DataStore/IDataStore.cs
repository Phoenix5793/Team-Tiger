using System.Collections.Generic;

namespace Tiger_YH_Admin.DataStore
{
    public interface IDataStore<T> where T : class
    {
        IEnumerable<T> Load();
        void Save();
        T AddItem(T item);
        void Remove(string itemId);
        T FindById(string id);
        IEnumerable<T> All();
    }
}
