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
    public partial class WaitList : Form
    {
        string admin;
        public WaitList(string tmp)
        {
            InitializeComponent();
            admin = tmp;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                Database.waitlist tmp = new Database.waitlist();
                Database.historique tmp2 = new Database.historique();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected == true)
                    {
                        tmp.remove(dataGridView1.Rows[i]);
                        tmp2.add(dataGridView1.Rows[i]);
                    }
                }
            }
            else
                MessageBox.Show("La liste est vide");
            refresh();
        }
        void refresh()
        {
            DataTable dt = new DataTable();
            Database.waitlist tmp = new Database.waitlist();

            dt = tmp.refresh();
            dataGridView1.DataSource = dt;

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            textBox2.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            //autocomplete the textbox with names
            for (int i=0; i<dataGridView1.Rows.Count; i++)
            {
                if (!textBox1.AutoCompleteCustomSource.Contains((dataGridView1.Rows[i].Cells[4].Value.ToString())))
                textBox1.AutoCompleteCustomSource.Add(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }


            //autocomplete the textbox with bill numbers
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!textBox2.AutoCompleteCustomSource.Contains((dataGridView1.Rows[i].Cells[6].Value.ToString())))
                    textBox2.AutoCompleteCustomSource.Add(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }

            if (dataGridView1.Rows.Count == 0)
                button1.Enabled = false;
            else
                button1.Enabled = true;

            if (admin != "MAGAZINIER")
            {
                button1.Enabled = false;
                button1.Visible = false;
            }


            dataGridView1.Columns[6].Name = "Numero de vente";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        }

        private void WaitList_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for(int i=0; i<dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells[4].Value.ToString()==textBox1.Text)
                {
                    dataGridView1.Rows[i].Selected = true;
                }
                else
                    dataGridView1.Rows[i].Selected = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[6].Value.ToString() == textBox2.Text)
                {
                    dataGridView1.Rows[i].Selected = true;
                }
                else
                    dataGridView1.Rows[i].Selected = false;
            }
        }
    }
}
