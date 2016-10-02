using Projeto_SR.Model;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.DataBase
{
    public class MessengerRepository
    {
        SQLiteAsyncConnection conn;

        public MessengerRepository(DbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task InsertMessengerAsync(Messenger messeger)
        {
            await conn.InsertOrReplaceWithChildrenAsync(messeger,recursive:true);
        }

        public async Task UpdateMessengerAsync(Messenger messeger)
        {
            await conn.UpdateWithChildrenAsync(messeger);
        }

        public async Task DeleteMessengerAsync(Messenger messeger)
        {
            await conn.DeleteAsync(messeger);
        }

        public async Task<List<Messenger>> SelectAllMessengerAsync()
        {
            
            return await conn.GetAllWithChildrenAsync<Messenger>(recursive: true);
        }

        public async Task<List<Messenger>> SelectMessengerAsync(string query)
        {
            return await conn.QueryAsync<Messenger>(query);
        }
    }
}
