namespace Tiger_YH_Admin.Models.Creators
{
	public interface ICreator<T> where T: class
	{
		T Create(IDataStore<T> dataStore);
	}
}
