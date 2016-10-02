using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.Model
{
    public class MessengerJson
    {
        public string mensagem { get; set; }

        public MessengerJson() { }

        public MessengerJson(string mensagem)
        {
            this.mensagem = mensagem;
        }
    }
}
