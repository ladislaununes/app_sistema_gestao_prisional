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
    public partial class FM_CadPenitenciaria : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_CadPenitenciaria()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand Guardar = new SqlCommand("INSERT INTO Penitenciaria VALUES (@nome, @local)", conexao);
            Guardar.CommandType = CommandType.Text;
            Guardar.Parameters.AddWithValue("@nome", txt_nome.Text);
            Guardar.Parameters.AddWithValue("@local", txt_local.Text);

            int t = Guardar.ExecuteNonQuery();
            if (t > 0)
                MessageBox.Show("Penitenciária salva com sucesso", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Falha ao salvar a penitenciária", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            conexao.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(txt_campo_pesquisa.Text);

                SqlConnection conexao = new SqlConnection(bd.Caminho);
                conexao.Open();
                SqlCommand Editar = new SqlCommand("UPDATE Penitenciaria SET nome = @nome, local_penitenciaria = @local WHERE id = @ID", conexao);
                Editar.CommandType = CommandType.Text;
                Editar.Parameters.AddWithValue("@nome", txt_nome.Text);
                Editar.Parameters.AddWithValue("@local", txt_local.Text);
                Editar.Parameters.AddWithValue("@ID", ID);
                int x = Editar.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Actualizado com sucesso!", "Actualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao actualizar os dados da Penitenciária", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexao.Close();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txt_campo_pesquisa.Text = "";
            txt_nome.Text = "";
            txt_local.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(txt_campo_pesquisa.Text);
                SqlConnection conexao = new SqlConnection(bd.Caminho);
                conexao.Open();
                SqlCommand pesquisar = new SqlCommand("SELECT * FROM Penitenciaria WHERE id = @ID", conexao);
                pesquisar.CommandType = CommandType.Text;
                pesquisar.Parameters.AddWithValue("@ID", ID);
                SqlDataReader ler = pesquisar.ExecuteReader();
                if (ler.Read())
                {
                    txt_nome.Text = ler["nome"].ToString();
                    txt_local.Text = ler["local_penitenciaria"].ToString();
                }
                else
                {
                    MessageBox.Show("Penitenciaria não localizada", "Pesquisa de Penitenciária", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FM_CadPenitenciaria_Load(object sender, EventArgs e)
        {

        }
    }
}
