using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    public abstract class DataStore<T> : IDataStore<T> where T : class
    {
        private string _fileName;
        private const string FilePrefix = "Data/";

        protected IEnumerable<T> DataSet { get; set; }

        protected DataStore()
        {
            InitializeState();
            DataSet = Load();
        }

        protected DataStore(IEnumerable<T> items)
        {
            InitializeState();
            DataSet = items;
        }

        private void InitializeState()
        {
            _fileName = FilePrefix + typeof(T).Name + "List.csv";
        }

        public abstract T FindById(string id);

        public IEnumerable<T> All()
        {
            return DataSet;
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

        public T AddItem(T item)
        {
            var itemList = DataSet.ToList();
            itemList.Add(item);
            DataSet = itemList;
            Save();

            return item;
        }

        public void Remove(string itemId)
        {
            List<T> itemList = DataSet.ToList();
            T item = FindById(itemId);
            itemList.Remove(item);
            DataSet = itemList;
        }
    }
}
