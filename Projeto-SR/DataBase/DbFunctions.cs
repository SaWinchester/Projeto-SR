using Projeto_SR.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.DataBase
{
    public class DbFunctions
    {

        public static async Task<MessengerRepository> obtem_conexao_bd()
        {
            DbConnection db = new DbConnection();
            await db.InitializeDatabase();
            MessengerRepository mr = new MessengerRepository(db);
            return mr;
        }

        public static async Task<List<Messenger>> obtem_messeger(MessengerRepository mr)
        {
            try
            {
                return await mr.SelectAllMessengerAsync();
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                return null;
            }

        }

        public static async void salva_messenger(Messenger messeger, MessengerRepository mr)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.2));
            await mr.InsertMessengerAsync(messeger);
        }
    }
}
