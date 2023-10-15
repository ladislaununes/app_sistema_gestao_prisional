using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sistema_de_Gestão_Prisional
{
    public partial class FM_CadFuncionario : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_CadFuncionario()
        {
            InitializeComponent();
        }

        private void btn_Fechar_Click(object sender, EventArgs e)
        {
            txt_nome.Text = "";
            txt_usuario.Text = "";
            txt_senha.Text = "";
            txt_Nidentificacao.Text = "";
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Guardar = new SqlCommand("INSERT INTO Funcionario VALUES (@nome, @usuario, @senha)", conexao);
            Guardar.CommandType = CommandType.Text;
            Guardar.Parameters.AddWithValue("@nome", txt_nome.Text);
            Guardar.Parameters.AddWithValue("@usuario", txt_usuario.Text);
            Guardar.Parameters.AddWithValue("@senha", txt_senha.Text);

            int t = Guardar.ExecuteNonQuery();
            if (t > 0)
                MessageBox.Show("Funcionário salvo com sucesso", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Falha ao salvar o funcionário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            conexao.Close();
        }   

        private void btn_Pesquisar_Click(object sender, EventArgs e)
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand pesquisar = new SqlCommand("SELECT * FROM Funcionario WHERE id=@N_identificacao", Conexao);
            pesquisar.CommandType = CommandType.Text;
            pesquisar.Parameters.AddWithValue("@N_identificacao", txt_Nidentificacao.Text);
            SqlDataReader ler = pesquisar.ExecuteReader();
            
            if (ler.Read()) {
                txt_nome.Text = ler["nome"].ToString();
                txt_usuario.Text = ler["usuario"].ToString();
                txt_senha.Text = ler["senha"].ToString();
            } else {
                MessageBox.Show("Dados não localizados.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(txt_Nidentificacao.Text);

                SqlConnection conexao = new SqlConnection(bd.Caminho);
                conexao.Open();
                SqlCommand Editar = new SqlCommand("UPDATE Funcionario SET nome = @nome, usuario = @usuario, senha = @senha WHERE id = @ID", conexao);
                Editar.CommandType = CommandType.Text;
                Editar.Parameters.AddWithValue("@nome", txt_nome.Text);
                Editar.Parameters.AddWithValue("@usuario", txt_usuario.Text);
                Editar.Parameters.AddWithValue("@senha", txt_senha.Text);
                Editar.Parameters.AddWithValue("@ID", ID);
                int x = Editar.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Actualizado com sucesso!", "Actualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao actualizar os dados do Funcionário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexao.Close();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

