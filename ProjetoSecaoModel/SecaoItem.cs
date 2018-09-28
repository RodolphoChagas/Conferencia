using PetaPoco;

namespace ProjetoSecaoModel
{
    [TableName("Conferencia_Secao_Item")]
    public class SecaoItem
    {

        [Column("secao_id")]
        public int SecaoId { get; set; }
        public string Isbn { get; set; }
        public int Quantidade { get; set; }

        [Ignore]
        public Secao Secao { get; set; }
    }
}
