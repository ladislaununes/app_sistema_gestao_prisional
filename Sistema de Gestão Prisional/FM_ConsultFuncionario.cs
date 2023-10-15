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
    public partial class FM_ConsultFuncionario : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_ConsultFuncionario()
        {
            InitializeComponent();
        }

        private void FM_ConsultFuncionario_Load(object sender, EventArgs e)
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT id AS 'COD', nome AS 'Nome Completo', usuario AS 'Nome de Usuário', senha AS 'Senha' FROM Funcionario", Conexao);
            Dep.CommandType = CommandType.Text;
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            Conexao.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT id AS 'COD', nome AS 'Nome Completo', usuario AS 'Nome de Usuário', senha AS 'Senha' FROM Funcionario WHERE nome LIKE @campo_pesquisa", Conexao);
            Dep.CommandType = CommandType.Text;
            Dep.Parameters.AddWithValue("@campo_pesquisa", '%'+txt_pesquisa.Text+'%');
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            Conexao.Close();
        }
    }
}
