using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestão_Prisional
{
    class BD
    {
        private String caminho = @"Data Source=CELSOALBERTO\SQLEXPRESS;Initial Catalog=sigere;Integrated Security=True";
        public String Caminho
        {
            get { return caminho; }
        }
    }
}
