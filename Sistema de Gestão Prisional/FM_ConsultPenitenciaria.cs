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
    public partial class FM_ConsultPenitenciaria : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_ConsultPenitenciaria()
        {
            InitializeComponent();
        }

        private void FM_ConsultDep_Load(object sender, EventArgs e)
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT nome AS 'Nome da Penitenciária', local_penitenciaria AS 'Localização' FROM Penitenciaria", Conexao);
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
            SqlCommand Dep = new SqlCommand("SELECT nome AS 'Nome da Penitenciária', local_penitenciaria AS 'Localização' FROM Penitenciaria WHERE nome = @campo_pesquisa", Conexao);
            Dep.CommandType = CommandType.Text;
            Dep.Parameters.AddWithValue("@campo_pesquisa", '%'+txt_CodDep.Text+'%');
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            Conexao.Close();
        }
    }
}
