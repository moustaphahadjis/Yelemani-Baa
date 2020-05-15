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
    public partial class Login : Form
    {
        Home home;
        public Login(Home ho)
        {
            InitializeComponent();
            home = ho;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.user tmp = new Database.user();
            DataTable dt = tmp.checkuser(textBox1.Text, textBox2.Text);

            if(dt.Rows.Count!=0)
            {
                Form1 form1 = new Form1(dt.Rows[0].ItemArray[0].ToString(), dt.Rows[0].ItemArray[1].ToString(), this, textBox1.Text,textBox2.Text);
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("NULL");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            home.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.user tmp = new Database.user();

            string rappel = tmp.getrappel(textBox1.Text);
            if (rappel != null)
                MessageBox.Show("Votre mot de rappel est " + rappel);
            else
                MessageBox.Show("Ce nom d'utilisateur n'existe pas dans la Base de Données");
        }
    }
}
