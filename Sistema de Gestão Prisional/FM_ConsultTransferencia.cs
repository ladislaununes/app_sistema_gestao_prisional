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
    public partial class FM_ConsultTransferencia : Form
    {
        //Instanciando a classe da Base de Dados
        BD bd = new BD();

        public FM_ConsultTransferencia()
        {
            InitializeComponent();
        }

        private void FM_ConsultTransferencia_Load(object sender, EventArgs e)
        {
            SqlConnection Conexao = new SqlConnection(bd.Caminho);
            Conexao.Open();
            SqlCommand Localizar = new SqlCommand("SELECT R.nome AS 'Nome do Recluso', P.nome AS 'Nome da Penitenciária', P.local_penitenciaria AS 'Município', T.data_transferencia AS 'Data da Transferência' FROM Transferencia T INNER JOIN Recluso R ON T.cod_recluso = R.id INNER JOIN Penitenciaria P ON T.cod_penitenciaria = P.id; ", Conexao);
            Localizar.CommandType = CommandType.Text;
            SqlDataReader Ler = Localizar.ExecuteReader();
            DataTable Dt = new DataTable();
            Dt.Load(Ler);
            dataGridView1.DataSource = Dt;
            Conexao.Close();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {

        }
    }
}
