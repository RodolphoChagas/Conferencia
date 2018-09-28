using PetaPoco;

namespace ProjetoSecaoModel
{
    [PrimaryKey("codpro")]
    public class Produto
    {
        [Column("codpro")]
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
    }
}
