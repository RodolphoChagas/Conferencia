using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace ProjetoSecaoModel
{
    [TableName("ConferenciaEstoque")]
    [PrimaryKey("CodConferencia")]
    public class Conferencia
    {
        
        public int codConferencia { get; set; }
        public string descricao { get; set; }
        public int secaoQtd { get; set; }
        [PetaPoco.Ignore]
        public List<Secao> lstSecoes { get; set; }
        public DateTime data { get; set; }
        public int status { get; set; }

        public Conferencia() { }
    }
}
