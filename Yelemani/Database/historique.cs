using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Yelemani.Database
{
    class historique
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public historique()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root;password=;database=yelemani");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }
        public DataTable getChartValue()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom_produit, Quantite,Date from historique", con);
            DataTable ds = new DataTable();

            da.Fill(ds);

            con.Close();
            return ds;
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom_produit, Quantite, Prix, Date from historique", con);
            DataTable ds = new DataTable();

            da.Fill(ds);

            con.Close();
            return ds;
        }
        public void add(DataGridViewRow dr)
        {
            cmd = new MySqlCommand("insert into historique (nom_produit, quantite, prix, vendeur, client, Payement, date,num) value('" + dr.Cells[0].Value.ToString() + "','" + Convert.ToInt32(dr.Cells[1].Value.ToString()) + "','" + Convert.ToDouble(dr.Cells[2].Value.ToString()) + "','" + dr.Cells[3].Value.ToString() + "','" + dr.Cells[4].Value.ToString() + "','" + dr.Cells[5].Value.ToString() + "','" + DateTime.Now.ToString() + "','" + dr.Cells[6].Value.ToString() + "')", con);
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
