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
    public partial class FM_ConsultRecluso : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_ConsultRecluso()
        {
            InitializeComponent();
        }

        private void FM_ConsultRecluso_Load(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT id AS 'COD', nome AS 'Nome Completo', genero AS 'Gênero', data_nascimento As 'Data de Nascimento', estado_civil AS 'Estado Civil', bi AS 'Nº de BI', nacionalidade AS 'Nacionalidade', residencia AS 'Residência' FROM Recluso", conexao);
            Dep.CommandType = CommandType.Text;
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            conexao.Close();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT id AS 'COD', nome AS 'Nome Completo', genero AS 'Gênero', data_nascimento As 'Data de Nascimento', estado_civil AS 'Estado Civil', bi AS 'Nº de BI', nacionalidade AS 'Nacionalidade', residencia AS 'Residência' FROM Recluso WHERE nome LIKE @id_recluso", conexao);
            Dep.CommandType = CommandType.Text;
            Dep.Parameters.AddWithValue("@id_recluso", "%"+txt_pesquisa.Text+"%");
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            conexao.Close();
        }
    }
}
