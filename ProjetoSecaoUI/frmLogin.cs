using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetoSecaoDao;
using ProjetoSecaoModel;


namespace ProjetoSecaoUI
{
    public partial class frmLogin : Form
    {

        public bool LogonSuccessful;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void ValidaUsuarioeSenha()
        {

            if (txtLogin.Text.Trim().Length == 0)
            {
                MessageBox.Show("Entre com o nome do usuário.", "Erro - Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtSenha.Text.Trim().Length == 0)
            {
                MessageBox.Show("Entre com a senha do usuário.", "Erro - Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!(new UsuarioDao()).SelecionaUsuario(txtLogin.Text, txtSenha.Text))
                {
                    MessageBox.Show("Nome do usuário ou senha incorretos.", "Erro Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado ao se comunicar com o banco de dados!" + Environment.NewLine + "Erro: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LogonSuccessful = true;
            Close();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ValidaUsuarioeSenha();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ValidaUsuarioeSenha();
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ValidaUsuarioeSenha();
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            LogonSuccessful = false;
            this.Close();
        }

        private void txtSenha_Click(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }
    }
}
