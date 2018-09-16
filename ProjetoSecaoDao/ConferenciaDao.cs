using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoSecaoModel;

namespace ProjetoSecaoDao
{
    public class ConferenciaDao
    {
        public List<Conferencia> GetConferencias()
        {
            List<Conferencia> conferencias;

            try
            {
                conferencias = (new PetaPoco.Database("stringConexao")).Query<Conferencia>("SELECT * FROM ConferenciaEstoque ORDER BY Data desc").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados das Conferências no Banco de Dados.");
            }

            return conferencias;
        }


        public Conferencia GetConferencias(int codConferencia)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Conferencia>("SELECT * FROM ConferenciaEstoque WHERE CodConferencia = " + codConferencia);
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao buscar esta Conferência no banco de dados, informe ao administrador do sistema!");
            }
        }


        public bool InsereConferencia(Conferencia conferencia)
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Insert("Movdb", "NumDoc", true, pedido);
                (new PetaPoco.Database("stringConexao")).Insert(conferencia);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool DeletaConferencia(Conferencia conferencia)
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Insert("Movdb", "NumDoc", true, pedido);
                (new PetaPoco.Database("stringConexao")).Delete(conferencia);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public ConferenciaDao() { }
    }
}
