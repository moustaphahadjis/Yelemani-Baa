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
    public partial class Form1 : Form
    {

        string name, admin;

        string username, password;
        Login login;
        Form activeForm = null;
        public Form1(string str1, string str2, Login log, string username1, string password1)
        {
            InitializeComponent();

            name = str1;
            admin = str2;
            login = log;

            username = username1;
            password = password1;
        }

        void secretaire()
        {
            panel6.Enabled = false;
            button10.Enabled = false;
            button14.Enabled = false;

            button10.Visible = false;
            panel6.Visible = false;
            button14.Visible = false;
        }
        void magazinier()
        {
           
            button4.Enabled = false;
            button10.Enabled = false;
            button14.Enabled = false;

            panel2.Enabled = false;
            panel3.Enabled = false;
            panel6.Enabled = false;

            
            button4.Visible = false;
            button10.Visible = false;
            button14.Visible = false;

            panel2.Visible = false;
            panel3.Visible = false;
            panel6.Visible = false;
        }
        void comptable()
        {
            button2.Enabled = false;
            button10.Enabled = false;
            
            button10.Visible = false;
            button2.Visible = false;

            panel2.Height=80;
        }
        void commercial()
        {
            button10.Enabled = false;

            button10.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if(admin=="SECRETAIRE")
            {
                secretaire();
            }
            else if(admin=="MAGAZINIER")
            {
                magazinier();
            }
            else if(admin=="COMPTABLE")
            {
                comptable();
            }
            else if (admin == "COMMERCIAL")
            {
                commercial();
            }

            if (panel2.Enabled == true)
                panel2.Visible = false;
            if (panel3.Enabled == true)
                panel3.Visible = false;
            if (panel6.Enabled == true)
                panel6.Visible = false;

        }

        void showTitle(Button btn)
        {
            label2.Text = btn.Text;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (admin == "MAGAZINIER")
            {
                changeForm(new Entrees(admin));
                showTitle(button2);
            }
            else if(admin=="SECRETAIRE")
            {
                changeForm(new Stock(admin));
                showTitle(button13);
            }
            else
            {
                if (panel2.Enabled == true)
                    panel2.Visible = true;
                if (panel3.Enabled == true)
                    panel3.Visible = false;
                if (panel6.Enabled == true)
                    panel6.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel2.Enabled == true)
                panel2.Visible = false;
            if (panel3.Enabled == true)
                panel3.Visible = true;
            if (panel6.Enabled == true)
                panel6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeForm(new Entrees(admin));
            showTitle(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            changeForm(new Sortie(admin));
            showTitle(button3);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (panel2.Enabled == true)
                panel2.Visible = false;
            if (panel3.Enabled == true)
                panel3.Visible = false;
            if (panel6.Enabled == true)
                panel6.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            changeForm(new WaitList(admin));
            showTitle(button11);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            changeForm(new AllUser());
            showTitle(button7);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            changeForm(new Historique());
            showTitle(button12);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            changeForm(new Clients(name, admin));
            showTitle(button6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            changeForm(new NewUser());
            showTitle(button8);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changeForm(new Fournisseur(name, admin));
            showTitle(button5);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            changeForm(new Stock(admin));
            showTitle(button13);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            changeForm(new Comptabilite());
            showTitle(button14);
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            MonCompte monCompte = new MonCompte(username, password);
            monCompte.ShowDialog();

        }

        void changeForm(Form newForm)
        {
            if(activeForm!=null)
            {
                activeForm.Close();
            }
            activeForm = newForm;
            activeForm.FormBorderStyle = FormBorderStyle.None;
            activeForm.BackColor = panel5.BackColor;
            activeForm.TopLevel = false;
            activeForm.Dock = DockStyle.Fill;
            panel5.Controls.Add(activeForm);
            activeForm.Show();
        }
    }
}
