using System.IO;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Projeto_SR.Model;

namespace Projeto_SR.DataBase
{
    public class DbConnection
    {
        string dbPath;
        SQLiteAsyncConnection conn;

        public DbConnection()
        {
            dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "falajob.sqlite");

            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)));
            conn = new SQLiteAsyncConnection(connectionFactory);
        }

        public async Task InitializeDatabase()
        {
            await conn.CreateTableAsync<Messenger>();
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return conn;
        }

    }
}
    