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
    public partial class FM_CadRecluso : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_CadRecluso()
        {
            InitializeComponent();
        }

        private void FM_CadRecluso_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Application.StartupPath.ToString() + "\\fotos\\semfoto.png";
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            try {
                caixa_de_dialogo.ShowDialog();
            
                Bitmap bmp = new Bitmap(caixa_de_dialogo.FileName);
                Bitmap bmp2 = new Bitmap(bmp, pictureBox1.Size);

                pictureBox1.Image = bmp2;
                pictureBox1.Image.Save(Application.StartupPath.ToString() + "\\fotos\\" + txt_nome.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
                txt_url.Text = Application.StartupPath.ToString() + "\\fotos\\" + txt_nome.Text + ".png";
            } catch (Exception) {
                MessageBox.Show("Operação cancelada!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho); 
            conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT R.nome, R.genero, R.data_nascimento, R.estado_civil, R.bi, R.nacionalidade, R.residencia, R.foto, P.tipo_crime, P.data_crime, P.nome_queixoso FROM Processo_Recluso PR INNER JOIN Recluso R ON PR.cod_recluso = R.id INNER JOIN Processo  P ON PR.cod_processo = P.num_processo WHERE PR.cod_processo = @num_processo", conexao);
            comando.CommandType = CommandType.Text;
            comando.Parameters.AddWithValue("@num_processo", txt_num_processo.Text);
            SqlDataReader ler = comando.ExecuteReader();

            if (ler.Read()) {
                txt_nome.Text = ler["nome"].ToString();
                dtp_data_nasc.Text = ler["data_nascimento"].ToString();
                cb_estado_civil.Text = ler["estado_civil"].ToString();
                txt_bi.Text = ler["bi"].ToString();
                txt_nacionalidade.Text = ler["nacionalidade"].ToString();
                txt_residencia.Text = ler["residencia"].ToString();
                txt_tipo_crime.Text = ler["tipo_crime"].ToString();
                dtp_data_crime.Text = ler["data_crime"].ToString();
                txt_queixoso.Text = ler["nome_queixoso"].ToString();
                txt_url.Text = ler["foto"].ToString();
                              
                if (ler["genero"].ToString() == "M"){
                    rb_masculino.Checked = true;
                    rb_feminino.Checked = false;
                } else {
                    rb_feminino.Checked = true;
                    rb_masculino.Checked = false;
                }

                if (txt_url.Text != "") {
                    try  {
                        Bitmap bmp = new Bitmap(txt_url.Text);
                        Bitmap bmp2 = new Bitmap(bmp, pictureBox1.Size);
                        pictureBox1.ImageLocation = txt_url.Text;
                    }
                    catch (Exception error) { MessageBox.Show(error.Message); }
                } else
                    pictureBox1.ImageLocation = Application.StartupPath.ToString() + "\\fotos\\semfoto.png";
            } else {
                MessageBox.Show("Dados não localizados!", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (txt_url.Text != "")
                    pictureBox1.ImageLocation = txt_url.Text;
                else
                    pictureBox1.ImageLocation = Application.StartupPath.ToString() + "\\fotos\\semfoto.png";
            }
            conexao.Close();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                char genero = '0';
                if (rb_masculino.Checked == true)
                    genero = 'M';
                else if (rb_feminino.Checked == true)
                    genero = 'F';
                SqlConnection conexao = new SqlConnection(bd.Caminho);
                conexao.Open();
                SqlCommand Guardar_Funcionario = new SqlCommand("INSERT INTO Recluso (nome, genero, data_nascimento, estado_civil, bi, nacionalidade, residencia, foto) VALUES (@nome, @genero, @data_nascimento, @estado_civil, @bi, @nacionalidade, @residencia, @foto)", conexao);
                Guardar_Funcionario.CommandType = CommandType.Text;
                Guardar_Funcionario.Parameters.AddWithValue("@nome", txt_nome.Text);
                Guardar_Funcionario.Parameters.AddWithValue("@genero", genero);
                Guardar_Funcionario.Parameters.AddWithValue("@data_nascimento", DateTime.Parse(dtp_data_nasc.Text));
                Guardar_Funcionario.Parameters.AddWithValue("@estado_civil", cb_estado_civil.Text);
                Guardar_Funcionario.Parameters.AddWithValue("@bi", txt_bi.Text);
                Guardar_Funcionario.Parameters.AddWithValue("@nacionalidade", txt_nacionalidade.Text);
                Guardar_Funcionario.Parameters.AddWithValue("@residencia", txt_residencia.Text);
                Guardar_Funcionario.Parameters.AddWithValue("@foto", txt_url.Text);

                int t = Guardar_Funcionario.ExecuteNonQuery();
                if (t > 0)
                {
                    SqlCommand Guardar_Processo = new SqlCommand("INSERT INTO Processo (tipo_crime, data_crime, nome_queixoso) VALUES (@tipo_crime, @data_crime, @nome_queixoso)", conexao);
                    Guardar_Processo.CommandType = CommandType.Text;
                    Guardar_Processo.Parameters.AddWithValue("@tipo_crime", txt_tipo_crime.Text);
                    Guardar_Processo.Parameters.AddWithValue("@data_crime", DateTime.Parse(dtp_data_crime.Text));
                    Guardar_Processo.Parameters.AddWithValue("@nome_queixoso", txt_queixoso.Text);
                    int p = Guardar_Processo.ExecuteNonQuery();
                    if (p > 0) {
                        SqlCommand Procurar_Processo = new SqlCommand("SELECT MAX(num_processo) AS 'max_num' FROM Processo", conexao);
                        Procurar_Processo.CommandType = CommandType.Text;
                        SqlDataReader l = Procurar_Processo.ExecuteReader();
                        string max_num = "";
                        if (l.Read()) {
                            max_num = l["max_num"].ToString();
                            l.Close();
                        }
                        SqlCommand Procurar_id = new SqlCommand("SELECT MAX(id) AS 'max_id' FROM Recluso", conexao);
                        Procurar_id.CommandType = CommandType.Text;
                        SqlDataReader ler = Procurar_id.ExecuteReader();
                        string max_id = "";
                        if (ler.Read()) {
                            max_id = ler["max_id"].ToString();
                            ler.Close();
                        }
                        SqlCommand Processo_Recluso = new SqlCommand("INSERT INTO Processo_Recluso VALUES (@processo, @recluso)", conexao);
                        Processo_Recluso.CommandType = CommandType.Text;
                        Processo_Recluso.Parameters.AddWithValue("@processo", max_num);
                        Processo_Recluso.Parameters.AddWithValue("@recluso", max_id);
                        int h = Processo_Recluso.ExecuteNonQuery();
                        if (h > 0) { MessageBox.Show("Recluso salvo com sucesso!", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else { MessageBox.Show("Falha ao salvar o recluso", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o recluso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexao.Close();
            }
            catch (Exception error) { MessageBox.Show(error.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_actualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
