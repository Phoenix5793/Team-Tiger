using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
	abstract class DataStore<T> where T: class
	{
		private readonly string _fileName;
		private const string FilePrefix = "Data/";

		public IEnumerable<T> DataSet { get; set; }

		protected DataStore()
		{
			_fileName = FilePrefix + typeof(T).Name + "List.csv";
			DataSet = Load();
		}

		public IEnumerable<T> Load()
		{
			var csv = new FileHelperEngine<T>(Encoding.UTF8);
			var records = csv.ReadFile(_fileName);
			return records;
		}

		public void Save()
		{
			var csv = new FileHelperEngine<T>(Encoding.UTF8);
			csv.WriteFile(_fileName, DataSet);
		}
	}
}
