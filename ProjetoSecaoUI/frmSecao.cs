using System;
using System.Drawing;
using System.Windows.Forms;
using ProjetoSecaoModel;
using ProjetoSecaoDao;

namespace ProjetoSecaoUI
{
    public partial class frmSecao : Form
    {
        private readonly Conferencia _conferencia;


        public frmSecao(Conferencia conferencia)
        {
            InitializeComponent();

            try
            {
                _conferencia = conferencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        public void AtualizaEListaSecoes()
        {
            _conferencia.Secoes = (new SecaoDao()).GetSecoesPorConferencia(_conferencia.ConferenciaId);

            GenerateTable(6, _conferencia.QuantidadeDeSecoes);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            tblSecao.Padding = new Padding(0, 0, vertScrollWidth, 0);

            AtualizaEListaSecoes();
        }

        private void GenerateTable(int columnCount, int secaoCount)
        {
            //Clear out the existing controls, we are generating a new table layout
            tblSecao.Controls.Clear();

            //Clear out the existing row and column styles
            tblSecao.ColumnStyles.Clear();
            tblSecao.RowStyles.Clear();
          

            //Now we will generate the table, setting up the row and column counts first
            tblSecao.ColumnCount = columnCount;
            //panel.RowCount = rowCount;

            for (int x = 0; x < columnCount; x++)
            {
                tblSecao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            }

            for (int w = 1; w <= secaoCount; w++)
            {

                var secao = _conferencia.Secoes.Find(x => x.NumeroDaSecao == w);
                

                var colorBotao = Color.FromArgb(161, 222, 33);
                var textoBotao = "Seção Pendente";


                if (secao != null)
                {
                    switch (secao.Status)
                    {
                        case "D":
                            colorBotao = Color.FromArgb(235, 219, 68);
                            textoBotao = "Seção em Contagem" + Environment.NewLine + Environment.NewLine + "Usuário: " + (secao.UsuarioId == Usuario.getInstance.codUser ? "Você": secao.NomeUsuario);
                            break;
                        case "F":
                            colorBotao = Color.FromArgb(237, 84, 84);
                            textoBotao = "Seção Finalizada" + Environment.NewLine + Environment.NewLine + "Usuário: " + (secao.UsuarioId == Usuario.getInstance.codUser ? "Você" : secao.NomeUsuario);
                            break;
                    }
                }

                //Create the control, in this case we will add a button
                var cmd = new Button();
                cmd.Size = new Size(132, 106);
                cmd.Font = new Font(cmd.Font, FontStyle.Bold);
                cmd.Cursor = Cursors.Hand;
                cmd.Margin = new Padding(3, 3, 3, 20);
                cmd.Tag = w; // Numero da seção
                cmd.Text = w + " - " + textoBotao;
                cmd.BackColor = colorBotao;

                tblSecao.Controls.Add(cmd);
                cmd.MouseDown += ControlsPanel_MouseDown;
            }
        }


        private void ControlsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            var numSecao = Convert.ToInt32(((Button)sender).Tag);

            var secao = _conferencia.Secoes.Find(x => x.NumeroDaSecao == numSecao);
            frmSecaoContagem frm;


            if (secao != null)
            {
                if (secao.UsuarioId != Usuario.getInstance.codUser)
                {
                    MessageBox.Show("Acesso negado!" + Environment.NewLine + "Essa Seção está em contagem por outro usuário!");
                    return;
                }

                frm = new frmSecaoContagem(numSecao, _conferencia.ConferenciaId, this, secao);
            }
            else
            {
                frm = new frmSecaoContagem(numSecao, _conferencia.ConferenciaId, this);
            }

            frm.ShowDialog();

            AtualizaEListaSecoes();
        }

        private void frmSecao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
    }
}
