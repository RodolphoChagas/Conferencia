using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using ProjetoSecaoModel;

namespace ProjetoSecaoDao
{
    public class SecaoDao
    {

        public List<Secao> GetSecoesPorConferencia(int conferenciaId)
        {
            List<Secao> secoes;

            try
            {
                using (var db = new Database("stringConexao"))
                {
                    secoes = db.Query<Secao>("SELECT secao_id, conferencia_id, Usuario.coduser as usuario_id, Conferencia_Secao.nome_usuario, numero_da_secao, status FROM Conferencia_Secao INNER JOIN Usuario on Conferencia_Secao.usuario_id = Usuario.CodUser WHERE conferencia_id=@0 ORDER BY numero_da_secao", conferenciaId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados das seções no Banco de Dados.");
            }

            return secoes;
        }

        public Secao GetSecao(int conferenciaId, int numeroDaSecao)
        {
            try
            {
                using (var db = new Database("stringConexao"))
                {
                    return db.SingleOrDefault<Secao>("SELECT * FROM Conferencia_Secao WHERE conferencia_id=@0 And numero_secao=@1", conferenciaId, numeroDaSecao);
                }
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao buscar esta Conferência no banco de dados, informe ao administrador do sistema!");
            }

        }
    }
}
