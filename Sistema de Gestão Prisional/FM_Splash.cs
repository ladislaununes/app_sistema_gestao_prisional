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
    public partial class FM_Splash : Form
    {
        public FM_Splash()
        {
            InitializeComponent();
        }

        private void tempo_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 100)
            {
                tempo.Stop();
                Form Login = new FM_Login();
                Login.Show();
                this.Hide();
            }
            else
            {
                lbl_tempo.Text = progressBar1.Value.ToString();
                progressBar1.Value += 4;
            }
        }
    }
}
