using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yelemani
{
    public partial class Sortie : Form
    {
        DataSet data_stock = new DataSet();
        DataSet data_client = new DataSet();
        DataTable data = new DataTable();
        string lieu;
        string admin;
        int num;
        public Sortie(string str)
        {
            InitializeComponent();
            refresh();
            data.Columns.Add("Nom");
            data.Columns.Add("Prix Unitaire");
            data.Columns.Add("Quantité");

            DataRow dr = data.NewRow();
            dataGridView1.DataSource = data;

            admin = str;
            num = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Sortie_Load(object sender, EventArgs e)
        {
            textBox3.ReadOnly = true;
            button4.Enabled = false;
        }
        void refresh()
        {
            Database.StockSortie tmp = new Database.StockSortie();
            Database.clients tmp2 = new Database.clients();


            data_stock = tmp.refresh();
            data_client = tmp2.refresh();

            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            textBox5.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            if (data_stock.Tables[0].Rows.Count > 1)
            {
                for (int i = 0; i < data_stock.Tables[0].Rows.Count; i++)
                {
                    textBox1.AutoCompleteCustomSource.Add(data_stock.Tables[0].Rows[i].ItemArray[0].ToString());
                }
            }

            if (data_stock.Tables[0].Rows.Count > 1)
            {
                for (int i = 0; i < data_client.Tables[0].Rows.Count; i++)
                {
                    textBox5.AutoCompleteCustomSource.Add(data_client.Tables[0].Rows[i].ItemArray[0].ToString());
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            bool exist = false;
            if((data_client.Tables[0].Rows.Count>1))
            for(int i=0; i<data_client.Tables[0].Rows.Count;i++)
            {
                if(data_client.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper() == textBox5.Text.ToUpper())
                {
                    textBox4.Text = data_client.Tables[0].Rows[i].ItemArray[1].ToString();
                    lieu = data_client.Tables[0].Rows[i].ItemArray[4].ToString();
                    exist = true;
                }
            }

            if (exist)
            {
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
                lieu = "Neant";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if( textBox2.Text.Trim()!="" && (Convert.ToDouble(textBox2.Text)>Convert.ToDouble(textBox6.Text)))
            {
                textBox2.Text = textBox6.Text;
                textBox2.SelectAll();
            }
        }

        private void button3_Click(object sender, EventArgs e)

        {
            if (textBox2.Text != "0")
            {
                if (textBox7.Text.Trim() != "" && textBox7.Text.All(char.IsDigit))
                {
                    DataRow dr = data.NewRow();
                    dr[0] = textBox1.Text;
                    dr[1] = textBox7.Text;
                    dr[2] = textBox2.Text;

                    data.Rows.Add(dr);
                }
                else
                {
                    MessageBox.Show("Veuillez vérifier le Prix Unitaire validé");
                }
            }
            else
                MessageBox.Show("Il n'y a plus de " + textBox1.Text + " en Stock");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for(int i=0; i<data_stock.Tables[0].Rows.Count;i++)
            {

                if(data_stock.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper()==textBox1.Text)
                {
                    textBox6.Text = data_stock.Tables[0].Rows[i].ItemArray[1].ToString();

                    textBox7.Text = data_stock.Tables[0].Rows[i].ItemArray[2].ToString();
                    textBox2.Text = "1";
                    break;
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double sum = 0;
            for(int i=0;i <dataGridView1.Rows.Count;i++)
            {
                sum += (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()));
            }
            textBox3.Text = sum.ToString();
        }

        bool newClient()
        {
            bool exist = false;
            for(int i=0; i<data_client.Tables[0].Rows.Count;i++)
            {
                if((data_client.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper().Trim()) == textBox5.Text.ToUpper().Trim() && data_client.Tables[0].Rows[i].ItemArray[1].ToString().ToUpper().Trim() == textBox4.Text.Trim() || (textBox4.Text.Trim()=="" && textBox5.Text==""))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        void vendre()
        {
            Database.waitlist tmp = new Database.waitlist();
            tmp.add(dataGridView1,textBox5.Text, admin,"Payé",num);
            DialogResult res = MessageBox.Show("Voulez-vous afficher le reçu?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Database.printOut print = new Database.printOut();
                print.print(dataGridView1, textBox5.Text, textBox3.Text, textBox4.Text, lieu);
            }
            refresh();
            reinit();
             
        }
        void reload()
        {
            for(int i= dataGridView1.Rows.Count-1; i>=0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }
            textBox5.Text = "";
            textBox1.Text = "";
        }

        void compte()
        {
            Database.StockSortie tmp2 = new Database.StockSortie();
            double solde = tmp2.checkBalance(textBox5.Text, textBox4.Text);
            double credit = tmp2.checkCredit(textBox5.Text, textBox4.Text);
            if (solde > Convert.ToDouble(textBox3.Text))
            {
                MessageBox.Show(" L'achat a été deduit du compte du client \n Son Solde est de " + solde);
                solde -= Convert.ToDouble(textBox3.Text);

                tmp2.setSolde(textBox5.Text, textBox4.Text, solde);
                Database.waitlist tmp = new Database.waitlist();
                tmp.add(dataGridView1, textBox5.Text, admin, "Compte", num);
                DialogResult res = MessageBox.Show("Voulez-vous afficher le reçu?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {

                    Database.printOut print = new Database.printOut();
                    print.print(dataGridView1, textBox5.Text, textBox3.Text, textBox4.Text, lieu);
                    refresh();
                    reinit();
                }
            }
            else
            {
                MessageBox.Show("Le compte du client est insuffisant");
            }
        }
        void credit()
        {

            Database.StockSortie tmp2 = new Database.StockSortie();
            double solde = tmp2.checkBalance(textBox5.Text, textBox4.Text);
            double credit = tmp2.checkCredit(textBox5.Text, textBox4.Text);
            
            
                MessageBox.Show(" L'achat est enregistré en tant que credit \n La dette du client est de " + credit);
                credit += Convert.ToDouble(textBox3.Text);

                tmp2.setCredit(textBox5.Text, textBox4.Text, credit);
                Database.waitlist tmp = new Database.waitlist();
                tmp.add(dataGridView1, textBox5.Text, admin, "Credit", num);
                DialogResult res = MessageBox.Show("Voulez-vous afficher le reçu?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {

                    Database.printOut print = new Database.printOut();
                    print.print(dataGridView1, textBox5.Text, textBox3.Text, textBox4.Text, lieu);
                    refresh();
                    reinit();
                }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //get and set Bill Number
            Database.Bill bill = new Database.Bill();
            num = bill.getNum();
            num++;
            bill.setNum(num);


            if (!newClient())
            {
                DialogResult res = MessageBox.Show("Voulez vous Enregister ce nouveau Client", "Ajouter nouveau", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    Database.clients tmp = new Database.clients();
                    tmp.add(textBox5.Text,"", textBox4.Text, 0, admin, DateTime.Now.ToString(),"Neant",0);
                    vendre();
                }
                else
                    vendre();
            }
            else
            {
                vendre();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //get and set Bill Number
            Database.Bill bill = new Database.Bill();
            num = bill.getNum();
            num++;
            bill.setNum(num);

            if (!newClient())
            {
               
                    Database.clients tmp = new Database.clients();
                    tmp.add(textBox5.Text,"", textBox4.Text, 0, admin, DateTime.Now.ToString(),"Neant",0);
                    credit();
                
            }
            else
            {
                credit();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for(int i=0; i<data_client.Tables[0].Rows.Count; i++)
            {
                if(data_client.Tables[0].Rows[i].ItemArray[0].ToString()!=textBox5.Text && data_client.Tables[0].Rows[i].ItemArray[1].ToString() != textBox4.Text)
                {
                    Database.clients tmp = new Database.clients();
                    tmp.add(textBox5.Text, textBox4.Text,admin);
                    MessageBox.Show("Nouveau Client Ajouté");

                    refresh();
                    button4.Enabled = false;
                    break;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(data_client.Tables[0].Rows.Count>1)
            for (int i = 0; i < data_client.Tables[0].Rows.Count; i++)
            {
                if (data_client.Tables[0].Rows[i].ItemArray[1].ToString().ToUpper() == textBox4.Text.ToUpper())
                {
                    textBox5.Text = data_client.Tables[0].Rows[i].ItemArray[0].ToString();
                }
            }
        }

        void reinit()
        {
            textBox1.Text = "";
            textBox2.Text = "0";
            textBox7.Text = "0";
            textBox5.Text = "";
            textBox4.Text = "";

            //dataGridView1.DataSource = null;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                dataGridView1.Rows.Remove(row);
            }

           
            textBox3.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get and set Bill Number
            Database.Bill bill = new Database.Bill();
            num = bill.getNum();
            num++;
            bill.setNum(num);

            if (!newClient())
            {

                Database.clients tmp = new Database.clients();
                tmp.add(textBox5.Text, "", textBox4.Text, 0, admin, DateTime.Now.ToString(), "Neant", 0);
                compte();

            }
            else
            {
                compte();
            }
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>=1)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
            //if(dataGridView1.SelectedRows.Count>0)
            //dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }
    }
}
