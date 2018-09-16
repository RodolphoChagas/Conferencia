using System;
using System.Windows.Forms;
using ProjetoSecaoDao;
using ProjetoSecaoModel;

namespace ProjetoSecaoUI
{
    public partial class frmSecaoContagem : Form
    {
        private readonly int _numSecao;
        private readonly int _codigoConferencia;

        public frmSecaoContagem(int numSecao, int codigoConferencia)
        {
            _numSecao = numSecao;
            _codigoConferencia = codigoConferencia;

            InitializeComponent();
        }

        private void frmSecaoContagem_Load(object sender, EventArgs e)
        {

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {

            


        }

        private void frmSecaoContagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}