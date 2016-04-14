using System.Collections.Generic;

namespace Tiger_YH_Admin.Models
{
	public interface IDataStore<T> where T: class
	{
		IEnumerable<T> DataSet { get; set; }
		IEnumerable<T> Load();
		void Save();
		T AddItem(T item);
		T FindById(string id);
	}
}
