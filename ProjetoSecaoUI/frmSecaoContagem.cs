using System;
using System.Windows.Forms;
using PetaPoco;
using ProjetoSecaoDao;
using ProjetoSecaoModel;

namespace ProjetoSecaoUI
{
    public partial class frmSecaoContagem : Form
    {
        private Secao _secao;
        private readonly frmSecao _frmSecao;
        private readonly int _conferenciaId;
        private readonly int _numeroSecao;


        public frmSecaoContagem(int numeroSecao, int conferenciaId, frmSecao frmSecao, Secao secao = null)
        {
            if(secao != null)
                _secao = secao;

            _frmSecao = frmSecao;
            _conferenciaId = conferenciaId;
            _numeroSecao = numeroSecao;


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

        private void txtIsbn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if ((new ProdutoDao()).GetProduto(txtIsbn.Text.Trim()) != null)
                {

                    var db = new Database("stringConexao");

                    try
                    {
                        db.BeginTransaction();

                        if (_secao == null)
                        {

                            _secao = new Secao()
                            {
                                ConferenciaId = _conferenciaId,
                                UsuarioId = Usuario.getInstance.codUser,
                                NomeUsuario = Usuario.getInstance.nomeUser,
                                Status = "D",
                                NumeroDaSecao = _numeroSecao,
                            };

                            _secao.SecaoId = Convert.ToInt32(db.Insert(_secao));
                        }


                        var item = new SecaoItem()
                        {
                            SecaoId = _secao.SecaoId,
                            Isbn = txtIsbn.Text.Trim(),
                            Quantidade = Convert.ToInt32(txtQuantidade.Text.Trim())
                        };

                        db.Insert(item);

                        db.CompleteTransaction();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar o produto no banco de dados, tente novamente." + Environment.NewLine + ex.Message);
                        db?.AbortTransaction();
                    }
                    finally
                    {
                        db?.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Produto não encontrado, tente novamente.");
                }


                txtQuantidade.Text = "1";
                txtIsbn.Text = "";
                txtIsbn.Focus();
                _frmSecao.AtualizaEListaSecoes();
            }
        }
    }
}