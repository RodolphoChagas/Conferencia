using PetaPoco;
using System.Collections;
using System.Collections.Generic;

namespace ProjetoSecaoModel
{
    [TableName("Conferencia_Secao")]
    [PrimaryKey("secao_id")]
    public class Secao
    {
        [Column("secao_id")]
        public int SecaoId { get; set; }
        [Column("conferencia_id")]
        public int ConferenciaId { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [Column("nome_usuario")]
        public string NomeUsuario { get; set; }
        [Column("numero_da_secao")]
        public int NumeroDaSecao { get; set; }
        public string Status { get; set; }

        [Ignore]
        public List<SecaoItem> Itens { get; set; }
    }
}
