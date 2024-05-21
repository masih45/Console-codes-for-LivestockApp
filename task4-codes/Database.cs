using SQLite;
using System.Collections.Generic;
using System.IO;

namespace comp609lecture3
{
    public class Database
    {
        private readonly SQLiteConnection _connection;

        public Database()
        {
            var dataDir = @"C:\Users\masih\Downloads";
            var dbPath = Path.Combine(dataDir, "FarmDataOriginal.db");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Cow>();
            _connection.CreateTable<Sheep>(); // Create tables if not exist
        }

        public List<Store> ReadItems()
        {
            var stores = new List<Store>();
            var cows = _connection.Table<Cow>().ToList();
            var sheep = _connection.Table<Sheep>().ToList();
            stores.AddRange(cows);
            stores.AddRange(sheep);
            return stores;
        }

        public int InsertItem(Store item)
        {
            return _connection.Insert(item);
        }

        public int DeleteItem(Store item)
        {
            return _connection.Delete(item);
        }

        public int UpdateItem(Store item)
        {
            return _connection.Update(item);
        }
    }
}
