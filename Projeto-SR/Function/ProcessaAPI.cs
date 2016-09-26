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
                string result = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return result;

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

                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(pergunta, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync(IP_SERVIDOR + "/mensagem", content);
                resposta = await response.Content.ReadAsStringAsync();

                return resposta;

            }
            catch (Exception e)
            {
                return ("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)");
            }
        }




    }
}
