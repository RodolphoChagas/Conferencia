using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace ProjetoSecaoModel
{
    [TableName("Conferencia")]
    [PrimaryKey("conferencia_id")]
    public class Conferencia
    {
        
        [Column("conferencia_id")]
        public int ConferenciaId { get; set; }
        public string Descricao { get; set; }
        [Column("quantidade_secoes")]
        public int QuantidadeDeSecoes { get; set; }
        public DateTime data { get; set; }
        public int status { get; set; }

        [PetaPoco.Ignore]
        public List<Secao> Secoes { get; set; }

        public Conferencia() { }
    }
}
