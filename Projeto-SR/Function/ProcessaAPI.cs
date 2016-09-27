using Newtonsoft.Json;
using Projeto_SR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.Functions
{
    class ProcessaAPI
    {

        private static string IP_SERVIDOR = "http://saw-falajobbr.rhcloud.com";

        public static async Task<string> first_connect()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(new Uri(IP_SERVIDOR + "/primeiraconexao"));
                var buffer = await response.Content.ReadAsByteArrayAsync();
                var byteArray = buffer.ToArray();
                string resposta = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

                Messenger messeger = JsonConvert.DeserializeObject<Messenger>(resposta);
                client.Dispose();
                return messeger.mensagem;

            }
            catch (Exception e)
            {
               return ("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)");
            }

        }

        public static async Task<string> get_serve_response(string pergunta)
        {
            try
            {
                string resposta = "";
                Messenger messenger = new Messenger(pergunta);
                string json = JsonConvert.SerializeObject(messenger);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync(IP_SERVIDOR + "/mensagem", content);
                var buffer = await response.Content.ReadAsByteArrayAsync();
                var byteArray = buffer.ToArray();
                resposta = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

                Messenger messeger = JsonConvert.DeserializeObject<Messenger>(resposta);
                client.Dispose();
                return messeger.mensagem;

            }
            catch (Exception e)
            {
                return ("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)");
            }
        }




    }
}
