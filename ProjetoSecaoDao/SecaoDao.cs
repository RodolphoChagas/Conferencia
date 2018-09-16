using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoSecaoModel;

namespace ProjetoSecaoDao
{
    public class SecaoDao
    {

        public List<Secao> GetSecoesPorConferencia(int codConferencia)
        {
            List<Secao> secoes;

            try
            {
                secoes = (new PetaPoco.Database("stringConexao")).Query<Secao>("SELECT codconferencia,Usuario.coduser,nomeuser,numsecao, conferenciasecao.condicao FROM ConferenciaSecao INNER JOIN Usuario on ConferenciaSecao.CodUser = Usuario.CodUser WHERE CodConferencia=@0 GROUP BY CodConferencia, Usuario.CodUser, nomeuser, numSecao, conferenciasecao.condicao ORDER BY NumSecao", codConferencia).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados das seções no Banco de Dados.");
            }

            return secoes;
        }

        public Secao GetSecao(int codConferencia, int numSecao)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Secao>("SELECT * FROM ConferenciaEstoque WHERE CodConferencia=@0 And numSecao=@1" , codConferencia, numSecao);
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao buscar esta Conferência no banco de dados, informe ao administrador do sistema!");
            }

        }
    }
}
