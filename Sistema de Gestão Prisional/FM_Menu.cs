using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Prisional
{
    public partial class FM_Menu : Form
    {
        public FM_Menu(string nome)
        {
            InitializeComponent();
            lbl_NomeFuncionario.Text = nome;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form CadFunc = new FM_CadFuncionario();
            CadFunc.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form ConsultFunc = new FM_ConsultFuncionario();
            ConsultFunc.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form cadRecluso = new FM_CadRecluso();
            cadRecluso.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Form consulRecluso = new FM_ConsultRecluso();
            consulRecluso.ShowDialog();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form cadSector = new FM_CadPenitenciaria();
            cadSector.ShowDialog();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form consultSector = new FM_ConsultPenitenciaria();
            consultSector.ShowDialog();
        }

        private void consultarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form ConsultEstoque = new FM_ConsultProcesso();
            ConsultEstoque.ShowDialog();
        }

        private void FM_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cadastrarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form cadastrar = new FM_CadTransferencia();
            cadastrar.ShowDialog();
        }

        private void consultarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form consultar = new FM_ConsultTransferencia();
            consultar.ShowDialog();
        }

        private void entradaDeReclusoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FM_EntradaRecluso = new FM_CadRecluso();
            FM_EntradaRecluso.ShowDialog();
        }

        private void reclusosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FM_Consultar_Processo = new FM_ConsultProcesso();
            FM_Consultar_Processo.ShowDialog();
        }
    }
}
