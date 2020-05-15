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
    public partial class AllUser : Form
    {
        List<string> deletedUsername;
        List<string> modifiedRow;
        List<string> modifiedTelephone;
        string Selectedrow;
        string selectedphone;

        public AllUser()
        {
            InitializeComponent();
            deletedUsername = new List<string>();
            modifiedRow = new List<string>();
            modifiedTelephone = new List<string>();

        }

        private void AllUser_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Database.user tmp = new Database.user();
            dt = tmp.refresh();

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            for(int i=0; i<dt.Rows.Count;i++)
            {
                textBox1.AutoCompleteCustomSource.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            dataGridView1.DataSource = dt;


            dataGridView1.Columns[6].ReadOnly = true;

            dataGridView1.Columns[3].HeaderText = "Nom d'Utilisateur";
            dataGridView1.Columns[4].HeaderText = "Mot de Passe";
            dataGridView1.Columns[6].HeaderText = "Statut";
        }
        void searchBar()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                textBox1.AutoCompleteCustomSource.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
        }
        void searchBarSelect()
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
        private void button3_Click(object sender, EventArgs e)
        {
            deletedUsername.Add(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

            

            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);

            MessageBox.Show("Utilisateur supprimé");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.user tmp = new Database.user();
            tmp.refresh();
            for (int i = 0; i < deletedUsername.Count; i++)
            {
                tmp.clear(deletedUsername[i]);
            }

            for(int i=0; i<modifiedRow.Count;i++)
            {
                //tmp.modify(Selectedrow, selectedphone);
            }
            deletedUsername.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Selectedrow = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                selectedphone = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
        }
        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Database.user tmp = new Database.user();
                tmp.update(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), dataGridView1.SelectedRows[0].Cells[5].Value.ToString(),dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), Selectedrow, selectedphone);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.ShowDialog();
        }
    }
}
