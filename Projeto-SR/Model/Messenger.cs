using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_SR.Model
{
    [Table("messenger")]
    public class Messenger
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string mensagem { get; set; }
        //P -> pergunta R -> Resposta
        public string type { get; set; }

        public int seq { get; set; }

        public Messenger() { }

        public Messenger(string mensagem, string type, int seq)
        {
            this.mensagem = mensagem;
            this.seq = seq;
            this.type = type;
        }
    }
}
