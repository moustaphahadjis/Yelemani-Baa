using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yelemani.Database;

namespace Yelemani
{
    public partial class MonCompte : Form
    {
        string username, password;
        public MonCompte(string username1, string password1)
        {
            InitializeComponent();
            username = username1;
            password = password1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox4.Text == textBox5.Text)
            {
                Database.user tmp = new user();
                tmp.update(textBox1.Text, textBox2.Text, textBox7.Text, textBox3.Text, textBox4.Text, textBox6.Text, username, password);

                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur \nVeuillez verifier les informations entrées");
            }
        }

        private void MonCompte_Load(object sender, EventArgs e)
        {
            Database.user tmp = new Database.user();
            DataTable dt = tmp.refresh(username, password);

            textBox1.Text = dt.Rows[0].ItemArray[0].ToString();
            textBox2.Text = dt.Rows[0].ItemArray[1].ToString();
            textBox3.Text = dt.Rows[0].ItemArray[3].ToString();
            textBox4.Text = dt.Rows[0].ItemArray[4].ToString();
            textBox5.Text = dt.Rows[0].ItemArray[4].ToString();
            textBox6.Text = dt.Rows[0].ItemArray[5].ToString();
            textBox7.Text = dt.Rows[0].ItemArray[2].ToString();
            comboBox1.Text = dt.Rows[0].ItemArray[6].ToString();

            comboBox1.Enabled = false;
        }
    }
}
