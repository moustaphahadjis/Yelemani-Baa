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
    public partial class Entrees : Form
    {
        DataTable data_f;
        DataTable data_stock;

        string admin;
        public Entrees(string admin1)
        {
            InitializeComponent();
            data_f = new DataTable();
            data_stock = new DataTable();
            admin = admin1;
            refresh();
        }

        private void Entrees_Load(object sender, EventArgs e)
        {
            reinit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void refresh()
        {

            Database.StockEntree tmp = new Database.StockEntree();

            data_stock = tmp.refresh().Tables[0];

            Database.fournisseur tmp2 = new Database.fournisseur();
            data_f = tmp2.refresh().Tables[0];

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            textBox2.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            textBox3.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            for (int i = 0; i < data_stock.Rows.Count; i++)
            {
                textBox1.AutoCompleteCustomSource.Add(data_stock.Rows[i].ItemArray[0].ToString());
            }

            for (int i = 0; i < data_f.Rows.Count; i++)
            {
                textBox2.AutoCompleteCustomSource.Add(data_f.Rows[i].ItemArray[0].ToString());
            }

            for (int i = 0; i < data_f.Rows.Count; i++)
            {
                textBox3.AutoCompleteCustomSource.Add(data_f.Rows[i].ItemArray[1].ToString());
            }

            if (admin == "MAGAZINIER")
            {
                button3.Enabled = true;
                button3.Visible = true;
            }
            else
            {
                button3.Enabled = false;
                button3.Visible = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                bool exist = false;
                for(int i=0; i<data_stock.Rows.Count;i++)
                {
                    if(data_stock.Rows[i].ItemArray[0].ToString()==textBox1.Text)
                    {
                        Database.StockEntree tmp = new Database.StockEntree();
                        tmp.update(textBox1.Text, textBox4.Text, data_stock.Rows[i].ItemArray[5].ToString());
                        MessageBox.Show(textBox4.Text + " " + comboBox1.Text + " de " + textBox1.Text + " ajoutés");
                        
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    Database.StockEntree tmp = new Database.StockEntree();
                    tmp.add(textBox1.Text, textBox2.Text, textBox3.Text, "Vers Stock", comboBox1.Text, textBox4.Text, textBox5.Text);
                    MessageBox.Show(textBox4.Text + " " + comboBox1.Text + " de " + textBox1.Text + " ajoutés");
                    
                }
            }
            else
            {
                MessageBox.Show("Vérifier que toutes les cases sont bien remplies");
            }
            reinit();
        }

       
        void reinit()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            comboBox1.SelectedItem = comboBox1.Items[0];
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reinit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            for(int i=0; i<data_f.Rows.Count;i++)
            {
                if(data_f.Rows[i].ItemArray[0].ToString()==textBox2.Text)
                {
                    textBox3.Text = data_f.Rows[i].ItemArray[1].ToString();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool exist = false;
            for(int i=0; i<data_stock.Rows.Count;i++)
            {
                if(data_stock.Rows[i].ItemArray[0].ToString()==textBox1.Text)
                {
                    textBox2.Text = data_stock.Rows[i].ItemArray[1].ToString();
                    textBox3.Text = data_stock.Rows[i].ItemArray[2].ToString();
                    comboBox1.Text = data_stock.Rows[i].ItemArray[4].ToString();
                    textBox4.Text = "1";
                    textBox5.Text = data_stock.Rows[i].ItemArray[6].ToString();
                    exist = true;
                }
            }
            if(exist)
            {
                comboBox1.Enabled = false;
                textBox5.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
            else
            {
                comboBox1.Enabled = true;
                textBox5.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stock tmp = new Stock(admin);
            tmp.ShowDialog();
        }
    }
}
