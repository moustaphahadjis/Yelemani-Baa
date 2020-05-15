using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Yelemani.Database
{
    class StockSortie
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public StockSortie()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root;password=;database=yelemani");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }
        public DataSet refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Quantite,Prix from stock", con);
            DataSet ds = new DataSet();

            da.Fill(ds);


            
            con.Close();
            return ds;
        }
        public void remove(string str1, int num)
        {
            int result;
            con.Open();

            cmd = new MySqlCommand("Select quantite from stock where nom='" + str1 + "'", con);
            MySqlDataReader rd= cmd.ExecuteReader();

            rd.Read();
            result = Convert.ToInt32(rd.GetValue(0).ToString());
            con.Close();

            con.Open();

            result -= num;
            cmd = new MySqlCommand("update stock set quantite='" + result + "' where nom='" + str1 + "'", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public double checkBalance(string str1, string str2)
        {
            con.Open();

            cmd = new MySqlCommand("Select solde from clients where nom='" + str1 + "' and telephone='" + str2 + "'", con);
            MySqlDataReader rd = cmd.ExecuteReader();
            double solde=0;
            if (rd.Read())
            {
                solde = Convert.ToDouble(rd.GetValue(0).ToString());
            }

            con.Close();
            return solde;

        }
        public double checkCredit(string str1, string str2)
        {
            con.Open();

            cmd = new MySqlCommand("Select credit from clients where nom='" + str1 + "' and telephone='" + str2 + "'", con);
            MySqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            double solde = Convert.ToDouble(rd.GetValue(0).ToString());

            con.Close();
            return solde;

        }
        public void setSolde(string name, string telephone, double solde)
        {
            con.Open();

            cmd = new MySqlCommand("update clients set solde='" + solde + "' where nom='" + name + "' and telephone='" + telephone + "'", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void setCredit(string name, string telephone, double credit)
        {
            con.Open();

            cmd = new MySqlCommand("update clients set credit='" + credit + "' where nom='" + name + "' and telephone='" + telephone + "'", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
