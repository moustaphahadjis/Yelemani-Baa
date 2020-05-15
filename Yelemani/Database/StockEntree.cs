using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Yelemani.Database
{
    class StockEntree
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public StockEntree()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root;password=;database=yelemani");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }
        public DataSet refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Fournisseur,Telephone,Utilisation,Unite,Quantite,Prix from stock", con);
            DataSet ds = new DataSet();

            da.Fill(ds);
            con.Close();
            return ds;
        }
        public void add(string str1, string str2, string str3, string str4, string str5, string str6, string str7)
        {
            //Change telephone type to string in database;

            


            cmd = new MySqlCommand("insert into stock (nom,fournisseur,telephone,utilisation,unite,quantite,prix) values ('" + str1 + "','" + str2 + "','" + str3 + "','" + str4 + "','" + str5 + "','" + Convert.ToInt32(str6) + "','" + Convert.ToDouble(str7) + "')",con);

            con.Open();

            cmd.ExecuteNonQuery();
            
            con.Close();
        }

        public void update(string name, string quantite, string quantite_initial)
        {
            int result = Convert.ToInt32(quantite) + Convert.ToInt32(quantite_initial);

            cmd = new MySqlCommand("update stock set quantite='" + result + "' where nom='" + name + "'",con);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
