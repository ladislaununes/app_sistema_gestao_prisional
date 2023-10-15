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
    public partial class FM_CadTransferencia : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_CadTransferencia()
        {
            InitializeComponent();
        }

        private void buscarRecluso()
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT id, nome FROM Recluso ORDER BY nome", Conexao);
            comando.CommandType = CommandType.Text;
            SqlDataReader Ler = comando.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(Ler);
            cb_recluso.DataSource = dt;
            cb_recluso.DisplayMember = "nome";
            cb_recluso.ValueMember = "id";
            Conexao.Close();   
        }

        private void buscarPenitenciaria()
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT id, nome FROM Penitenciaria ORDER BY nome", Conexao);
            comando.CommandType = CommandType.Text;
            SqlDataReader Ler = comando.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(Ler);
            cb_penitenciaria.DataSource = dt;
            cb_penitenciaria.DisplayMember = "nome";
            cb_penitenciaria.ValueMember = "id";
            Conexao.Close();   
        }

        private void FM_CadTransferencia_Load(object sender, EventArgs e)
        {
            buscarRecluso();
            buscarPenitenciaria();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int cod_recluso = int.Parse(cb_recluso.SelectedValue.ToString());
            int cod_penitenciaria = int.Parse(cb_penitenciaria.SelectedValue.ToString());
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Guardar = new SqlCommand("INSERT INTO Transferencia VALUES (@data_trans, @cod_recluso, @cod_penitenciaria)", conexao);
            Guardar.CommandType = CommandType.Text;
            Guardar.Parameters.AddWithValue("@data_trans", DateTime.Parse(dp_data_tranferencia.Text));
            Guardar.Parameters.AddWithValue("@cod_recluso", cod_recluso);
            Guardar.Parameters.AddWithValue("@cod_penitenciaria", cod_penitenciaria);

            int t = Guardar.ExecuteNonQuery();
            if (t > 0)
                MessageBox.Show("Transferência salva com sucesso", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Falha ao salvar o transferência", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            conexao.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
