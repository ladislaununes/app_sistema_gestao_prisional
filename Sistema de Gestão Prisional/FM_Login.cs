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
    public partial class FM_Login : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_Login()
        {
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(bd.Caminho);
            conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT nome, usuario, senha FROM Funcionario WHERE usuario = @usuario AND senha = @senha", conexao);
            comando.CommandType = CommandType.Text;
            comando.Parameters.AddWithValue("@usuario", txt_usuario.Text);
            comando.Parameters.AddWithValue("@senha", txt_senha.Text);
            SqlDataReader dt = comando.ExecuteReader();
            bool result = dt.Read();
            if (result == true) {
                Form Principal = new FM_Menu(dt["nome"].ToString());
                Principal.Show();
                this.Hide();
            } else {
                MessageBox.Show("Nome de usuário ou senha incorrecta!\nPor favor, tente novamente.", "Senha errada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_usuario.Text = "";
                txt_senha.Text = "";
            }
            conexao.Close();
        }

        private void FM_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
