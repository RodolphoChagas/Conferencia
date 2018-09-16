using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetoSecaoDao;
using ProjetoSecaoModel;

namespace ProjetoSecaoUI
{
    public partial class frmConferencia : Form
    {
        private List<Conferencia> _conferencia = new List<Conferencia>();

        public frmConferencia()
        {
            InitializeComponent();
        }

        private void frmConferencia_Load(object sender, EventArgs e)
        {
            lblDataAtual.Text = DateTime.Now.ToString();
            txtDescricaoConf.Select();

            InicializalstVwConferencia();
        }

        private void InicializalstVwConferencia()
        {

            _conferencia = (new ConferenciaDao()).GetConferencias();

            if (_conferencia == null)
                return;

            lstVWConferencia.Items.Clear();

            foreach (var conf in _conferencia)
            {
                var ls = new ListViewItem(conf.codConferencia.ToString());
                ls.SubItems.Add(conf.descricao);
                ls.SubItems.Add(conf.secaoQtd.ToString("00"));
                ls.SubItems.Add(conf.data.ToString());
                ls.SubItems.Add(conf.status == 0 ? "Aberta": "Finalizada");

                lstVWConferencia.Items.Add(ls);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtDescricaoConf.Text))
            {
                MessageBox.Show("Informe uma descrição para a conferência por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtSecaoQtdConf.Text))
            {
                MessageBox.Show("Informe a quantidade de seções por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var conferencia = new Conferencia
            {
                descricao = txtDescricaoConf.Text.Trim(),
                secaoQtd = Convert.ToInt16(txtSecaoQtdConf.Text),
                data = DateTime.Now,
                status = 0
            };




            var ret = (new ConferenciaDao()).InsereConferencia(conferencia);

            if (ret)
            {
                InicializalstVwConferencia();
            }
            else
            {
                MessageBox.Show("Houve um erro inesperado ao inserir os dados, informe ao administrador do sistema!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void visualizarSeçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstVWConferencia.FocusedItem == null)
                return;

            var conferencia = _conferencia[lstVWConferencia.FocusedItem.Index];

            var frm = new frmSecao(conferencia);
            frm.ShowDialog();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstVWConferencia.FocusedItem == null)
                return;

            var conferencia = _conferencia[lstVWConferencia.FocusedItem.Index];

            if (conferencia.status == 0)
            {
                if (MessageBox.Show("Essa conferência está ABERTA, confirma excluir mesmo assim ?", "Mensagem - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    return;
                }
            }


            var ret = (new ConferenciaDao()).DeletaConferencia(conferencia);

            if (ret)
            {
                InicializalstVwConferencia();
            }
            else
            {
                MessageBox.Show("Houve um erro inesperado ao excluir o dado selecionado, informe ao administrador do sistema!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            txtSecaoQtdConf.Text = string.Empty;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (lstVWConferencia.FocusedItem == null)
                return;

            var status = lstVWConferencia.FocusedItem.SubItems[4].Text;

            //if (status == "Fechado")
            //    visualizarSeçõesToolStripMenuItem.Enabled = false;
            //else
            //    visualizarSeçõesToolStripMenuItem.Enabled = true;

            visualizarSeçõesToolStripMenuItem.Enabled = status != "Fechado";
        }

        private void lstVWConferencia_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            if (lstVWConferencia.FocusedItem.Bounds.Contains(e.Location) == true)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }
    }
}
