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
    public partial class Comptabilite : Form
    {
        DataTable dt;

        public Comptabilite()
        {
            InitializeComponent();
        }
        void refresh()
        {
            Database.comptabilite com = new Database.comptabilite();
           dt = new DataTable();
            dt = com.refresh();

            dataGridView1.DataSource = dt;
        }
        private void dateTimePicker1_Validated(object sender, EventArgs e)
        {
            double price = 0, quantite = 0;
            for (int i=0; i<dt.Rows.Count;i++)
            {
                DateTime tmp = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString());

                if (DateTime.Compare(tmp, dateTimePicker1.Value) > 0 && DateTime.Compare(tmp, dateTimePicker2.Value) < 0)
                {
                    dataGridView1.Rows[i].Selected = true;
                    price += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    quantite += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
                else
                {
                    dataGridView1.Rows[i].Selected = false;
                    //dataGridView1.Rows[i].Visible = false;
                }
            }
            textBox1.Text = price.ToString();
            textBox2.Text = quantite.ToString();
        }

        private void Comptabilite_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            double price = 0, quantite = 0;

            for (int i=0; i<dataGridView1.SelectedRows.Count;i++)
            {
                price += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
                quantite += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }

            textBox1.Text = price.ToString();
            textBox2.Text = quantite.ToString();
        }
    }
}
