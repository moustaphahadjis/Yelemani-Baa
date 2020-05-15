using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yelemani
{
    public partial class Historique : Form
    {
        public Historique()
        {
            InitializeComponent();
        }

        private void Historique_Load(object sender, EventArgs e)
        {
            refresh();
        }
        void refresh()
        {
            Database.historique tmp = new Database.historique();
            dataGridView1.DataSource = tmp.refresh();
        }
    }
}
