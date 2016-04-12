using System.Collections.Generic;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
	class DataStore<T> where T: class
	{
		private readonly string _fileName;
		private const string FilePrefix = "Data/";

		public IEnumerable<T> DataSet { get; set; }

		public DataStore()
		{
			_fileName = FilePrefix + typeof(T).Name + "List.csv";
			DataSet = Load();
		}

		private IEnumerable<T> Load()
		{
			var csv = new FileHelperEngine<T>();
			var records = csv.ReadFile(_fileName);
			return records;
		}

		public void Save()
		{
			var csv = new FileHelperEngine<T>();
			csv.WriteFile(_fileName, DataSet);
		}
	}
}
