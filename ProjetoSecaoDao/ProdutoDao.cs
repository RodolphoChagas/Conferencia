using PetaPoco;
using ProjetoSecaoModel;
using System;

namespace ProjetoSecaoDao
{
    public class ProdutoDao
    {

        public Produto GetProduto(string isbn)
        {

            Produto produto;

            using (var db = new Database("stringConexao"))
            {
                try
                {
                    var sql = "SELECT codpro, descricao FROM Produto WHERE ISBN = '" + isbn + "'";

                    produto = db.SingleOrDefault<Produto>(sql);
                }
                catch (Exception ex)
                {
                    produto = null;
                }
            }

            return produto;
        }





    }
}
