using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoSecaoModel;

namespace ProjetoSecaoDao
{
    public class UsuarioDao
    {
        public bool SelecionaUsuario(string nomeuser, string senha)
        {
            var user = (new PetaPoco.Database("stringConexao")).SingleOrDefault<Usuario>("SELECT CodUser, NomeUser, Senha FROM Usuario WHERE NomeUser=@0 AND Senha=@1", nomeuser, senha);

            if (user == null) return false;

            Usuario.getInstance.codUser = user.codUser;
            Usuario.getInstance.nomeUser = user.nomeUser;
            Usuario.getInstance.senha = user.senha;

            return true;
        }
    }
}
