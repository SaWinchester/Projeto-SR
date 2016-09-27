using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.Model
{
    public class Messenger
    {
        public string mensagem { get; set; }

        Messenger() { }

        public Messenger(string mensagem)
        {
            this.mensagem = mensagem;
        }
    }
}
