using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Yelemani.Database
{
    class Bill
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public Bill()
        {
            //con = new MySqlConnection("datasource=192.168.120.11;port=3306;username=root; password=;database=yelemani;");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root; password=;database=yelemani;");
            con.Close();
        }

        public int getNum()
        {
            cmd = new MySqlCommand("select num from Bill",con);
            int result=0;
            try
            {
                con.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    result = read.GetInt32(0);
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection au serveur impossible");
                result = 0;
            }

            return result;
        }

        public void setNum(int num)
        {
            cmd = new MySqlCommand("update bill set num='" + num + "'",con);
            //cmd = new MySqlCommand("insert into bill (num) values('" + num + "')", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection au serveur impossible");
            }
        }
    }
}
