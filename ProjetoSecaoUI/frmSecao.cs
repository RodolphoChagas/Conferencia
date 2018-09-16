using System;
using System.Drawing;
using System.Windows.Forms;
using ProjetoSecaoModel;
using ProjetoSecaoDao;

namespace ProjetoSecaoUI
{
    public partial class frmSecao : Form
    {
        private readonly Conferencia _conferencia = new Conferencia();
        private readonly int _secoesQtd = 0;

        public frmSecao(Conferencia conferencia)
        {
            InitializeComponent();


            try
            {
                _conferencia = conferencia;
                _secoesQtd = conferencia.secaoQtd;

                _conferencia.lstSecoes = (new SecaoDao()).GetSecoesPorConferencia(_conferencia.codConferencia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            tblSecao.Padding = new Padding(0, 0, vertScrollWidth, 0);


            GenerateTable(6, _secoesQtd);
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

                var secao = _conferencia.lstSecoes.Find(x => x.numSecao == w);
                

                var colorBotao = Color.FromArgb(161, 222, 33);
                var textoBotao = "Seção Pendente";


                if (secao != null)
                {
                    switch (secao.condicao)
                    {
                        case "D":
                            colorBotao = Color.FromArgb(235, 219, 68);
                            textoBotao = "Seção em Contagem" + Environment.NewLine + Environment.NewLine + "Usuário: " +
                                         secao.nomeUser;
                            break;
                        case "F":
                            colorBotao = Color.FromArgb(237, 84, 84);
                            textoBotao = "Seção Finalizada" + Environment.NewLine + Environment.NewLine + "Usuário: " +
                                         secao.nomeUser;
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ControlsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            var numSecao = Convert.ToInt32(((Button)sender).Tag);

            var secao = _conferencia.lstSecoes.Find(x => x.numSecao == numSecao);

            if (secao != null)
            {
                //if (secao.codUser != Usuario.getInstance.codUser)
                //{
                //    MessageBox.Show("Acesso negado!" + Environment.NewLine + "Essa Seção está em contagem ou já foi finalizada por outro usuário!");
                //    return;
                //}
                if (secao.condicao == "D")
                {
                    MessageBox.Show("Acesso negado!" + Environment.NewLine + "Essa Seção está em contagem por outro usuário!");
                    return;
                }
            }

            var frm = new frmSecaoContagem(numSecao, 1);
            frm.ShowDialog();
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
