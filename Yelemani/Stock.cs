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
    public partial class Stock : Form
    {
        List<string> deletedUsername;
        List<string> deletedFournisseur;
        List<string> modifiedRow;
        List<string> modifiedTelephone;
        string Selectednom;
        string selectedfournisseur;

        string admin;
        public Stock(string tmp)
        {
            InitializeComponent();
            deletedUsername = new List<string>();

            deletedFournisseur = new List<string>();
            modifiedRow = new List<string>();
            modifiedTelephone = new List<string>();

            admin = tmp;
        }
        void refresh()
        {
            DataTable dt = new DataTable();
            Database.stock tmp = new Database.stock();
            dt = tmp.refresh();
            dataGridView1.DataSource = dt;


            if (admin == "ADMIN" || admin == "COMMERCIAL")
            {
                dataGridView1.ReadOnly = false;
                groupBox1.Enabled = true;
            }
            else
            {
                dataGridView1.ReadOnly = true;
                groupBox1.Enabled = false;
            }

            searchBar();

            int classik = 0;
            int autre = 0;
            int total = 0;

            for(int i=0; i< dt.Rows.Count; i++)
            {
                if(dt.Rows[i].ItemArray[0].ToString().Trim().Contains("CLASSIK"))
                {
                    classik += Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                }
                else
                {

                    autre += Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                }
            }
            total = autre + classik;
            textBox2.Text = classik.ToString();
            textBox3.Text = autre.ToString();
            textBox4.Text = total.ToString();
        }
        private void Stock_Load(object sender, EventArgs e)
        {
            refresh();
        }
        void searchBar()
        {
            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            if(dataGridView1.Rows.Count>1)
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                textBox1.AutoCompleteCustomSource.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
        }
        void searchBarSelect()
        {
            if(dataGridView1.Rows.Count>1)
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox1.Text)
                {
                    dataGridView1.Rows[i].Selected = true;
                    break;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Database.stock tmp = new Database.stock();
                tmp.delete(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                refresh();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.stock tmp = new Database.stock();
            tmp.refresh();
            for (int i = 0; i < deletedUsername.Count; i++)
            {
                tmp.clear(deletedUsername[i], deletedFournisseur[i]);
            }

            for (int i = 0; i < modifiedRow.Count; i++)
            {
                //tmp.modify(Selectedrow, selectedphone);
            }
            deletedUsername.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;

            if (dataGridView1.SelectedRows.Count != 0)
            {
                Selectednom = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                selectedfournisseur = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            modifiedRow.Add(Selectednom);
            modifiedTelephone.Add(selectedfournisseur);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox1.Text)
                {
                    dataGridView1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        string SelectedName;
        private void dataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            Database.stock tmp = new Database.stock();
            tmp.update(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()), Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[4].Value.ToString()), dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), SelectedName);
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                SelectedName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }
    }
}
