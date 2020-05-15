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
    public partial class Fournisseur : Form
    {
        string SelectedName, SelectedTelephone, SelectedSolde;
        string Admin, AdminName;
        public Fournisseur(string name, string admin)
        {
            InitializeComponent();
            AdminName = name;
            Admin = admin;

            if (Admin == "SECRETAIRE")
            {
                dataGridView1.ReadOnly = true;
                button3.Visible = false;
                button3.Enabled = false;

            }
        }

        private void Fournisseur_Load(object sender, EventArgs e)
        {
            refresh();
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
        }
        void refresh()
        {

            Database.fournisseur tmp = new Database.fournisseur();
            DataSet ds = new DataSet();

            ds = tmp.refresh();

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                textBox1.AutoCompleteCustomSource.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
            }
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.Columns[4].HeaderText = "Date";

        }
        void find(string nom, string telephone)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() == nom.Trim() && dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() == telephone.Trim())
                {
                    dataGridView1.Rows[i].Selected = true;
                }
            }
        }
        void find(string nom)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() == nom.Trim())
                {
                    dataGridView1.Rows[i].Selected = true;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Database.fournisseur tmp = new Database.fournisseur();
            tmp.delete(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            this.refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //checking name
            Database.fournisseur tmp = new Database.fournisseur();
            bool exist = false;

            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() == textBox2.Text.Trim() && dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() == textBox3.Text.Trim())
                    {
                        exist = true;
                        break;
                    }
                }

            if (!exist)
            {
                tmp.add(textBox2.Text, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text), AdminName, DateTime.Now.ToString(), textBox5.Text, textBox6.Text);
                this.refresh();
                find(textBox2.Text, textBox3.Text);
            }
            else
            {
                MessageBox.Show("Cet Nom et numero de Téléphone existent déjà dans la Base de données");
                find(textBox2.Text, textBox3.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                SelectedName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                SelectedTelephone = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Database.fournisseur tmp = new Database.fournisseur();
            tmp.update(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), SelectedName, SelectedTelephone, AdminName, DateTime.Now.ToString());
            this.refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            find(textBox1.Text);
        }
    }
}
