using Tiger_YH_Admin.DataStore;

namespace Tiger_YH_Admin.Creators
{
    public interface ICreator<T> where T : class
    {
        T Create(IDataStore<T> dataStore);
    }
}
