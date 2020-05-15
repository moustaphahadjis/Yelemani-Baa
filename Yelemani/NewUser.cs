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
    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim()!="" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox4.Text == textBox5.Text)
            {
                Database.user tmp = new Database.user();
                if (!tmp.chechusername(textBox3.Text))
                {
                    tmp.add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text, textBox6.Text, textBox7.Text);
                    MessageBox.Show("Nouvel utilisateur enregistré avec succès");
                }
                else
                {
                    MessageBox.Show("Un autre utilisateur utilise le meme pseudonyme\nVeuillez le changer");
                }
            }
            else
            {
                MessageBox.Show("Erreur \nVeuillez verifier les informations entrées");
            }

            reinit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void reinit()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = ""; 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            reinit();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }
    }
}
