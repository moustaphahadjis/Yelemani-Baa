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
    public partial class Home : Form
    {
        int tmp;
        Login login;

        public Home()
        {
            InitializeComponent();
            login = new Login(this);
            tmp = 0;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 5;
            if (panel2.Width >= panel1.Width)
            {

                login.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            //Database.Bill bill = new Database.Bill();
            //bill.setNum(1);
        }
    }
}
