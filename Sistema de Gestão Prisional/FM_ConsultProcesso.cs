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
    public partial class FM_ConsultProcesso : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_ConsultProcesso()
        {
            InitializeComponent();
        }
        private void FM_ConsultEstoque_Load(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Dep = new SqlCommand("SELECT R.nome AS 'Nome do Recluso', R.genero AS 'Gênero', R.data_nascimento AS 'Data de nascimento', R.estado_civil AS 'Estado civil', R.bi AS 'Nº do BI', R.nacionalidade AS 'Nacionalidade', R.residencia AS 'Residência', P.tipo_crime AS 'Tipo de crime', P.data_crime AS 'Data do crime', P.nome_queixoso AS 'Nome do Queixoso' FROM Processo_Recluso PR INNER JOIN Recluso R ON PR.cod_recluso = R.id INNER JOIN Processo  P ON PR.cod_processo = P.num_processo", conexao);
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
            SqlCommand Dep = new SqlCommand("SELECT R.nome AS 'Nome do Recluso', R.genero AS 'Gênero', R.data_nascimento AS 'Data de nascimento', R.estado_civil AS 'Estado civil', R.bi AS 'Nº do BI', R.nacionalidade AS 'Nacionalidade', R.residencia AS 'Residência', P.tipo_crime AS 'Tipo de crime', P.data_crime AS 'Data do crime', P.nome_queixoso AS 'Nome do Queixoso' FROM Processo_Recluso PR INNER JOIN Recluso R ON PR.cod_recluso = R.id INNER JOIN Processo  P ON PR.cod_processo = P.num_processo WHERE PR.cod_processo = @num_processo", conexao);
            Dep.CommandType = CommandType.Text;
            Dep.Parameters.AddWithValue("@num_processo", txt_num_processo.Text);
            SqlDataReader ler = Dep.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ler);
            dataGridView1.DataSource = dt;
            conexao.Close();
        }
    }
}
