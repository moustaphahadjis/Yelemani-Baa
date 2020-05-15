using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Yelemani.Database
{
    class waitlist
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public waitlist()
        {
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }

        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom_produit, Quantite, Prix, Vendeur, Client,Payement,num from waitlist", con);
            DataTable ds = new DataTable();

            da.Fill(ds);
            
            con.Close();
            return ds;
        }
        public void add(DataGridView dt,string str1, string str2,string str3,int billnum)
        {
            for(int i=0; i<dt.Rows.Count;i++)
            {
                int num = Convert.ToInt32(dt.Rows[i].Cells[2].Value.ToString());
                cmd = new MySqlCommand("Insert into waitlist(nom_produit, quantite, prix, vendeur, client,Payement,num) values ('" + dt.Rows[i].Cells[0].Value.ToString() + "','" + num + "','" + Convert.ToDouble(dt.Rows[i].Cells[2].Value.ToString())*num + "','" + str2 + "','" + str1 + "','" + str3 + "','"+billnum+"')", con);
                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
            
        }
        public void remove(DataGridViewRow dr)
        {
            Database.StockSortie tmp = new StockSortie();
            tmp.remove(dr.Cells[0].Value.ToString(), Convert.ToInt32(dr.Cells[1].Value.ToString()));
            cmd = new MySqlCommand("delete from waitlist where nom_produit='" + dr.Cells[0].Value.ToString() + "' and quantite='" + dr.Cells[1].Value.ToString() + "' and client='" + dr.Cells[4].Value.ToString() + "'", con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
