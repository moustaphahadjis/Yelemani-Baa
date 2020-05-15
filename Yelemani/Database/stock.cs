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
    class stock
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public stock()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root;password=;database=yelemani");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Fournisseur, Telephone,Quantite,Prix,Utilisation from stock", con);
            DataTable ds = new DataTable();

            da.Fill(ds);

            
            con.Close();
            return ds;
        }

        public DataTable getNames()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom from stock", con);
            DataTable ds = new DataTable();

            da.Fill(ds);


            con.Close();
            return ds;
        }
        public void update(string nom, string fournisseur, string telephone, int quantite, double prix,string utilisation,string selectedName)
        {
            cmd = new MySqlCommand("update stock set nom='" + nom + "',fournisseur='" + fournisseur + "',telephone='" + telephone + "',quantite='" + quantite + "',prix='"+prix+"',utilisation='" + utilisation + "' where nom='"+selectedName+"'", con);
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void clear(string nom, string fournisseur)
        {
            cmd = new MySqlCommand("delete from stock where nom='" + nom.ToUpper() + "' and fournisseur='" + fournisseur.ToUpper() + "'", con);
            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void delete(string nom)
        {
            cmd = new MySqlCommand("delete from stock where nom='" + nom.ToUpper() + "'", con);
            try
            {
                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Operation réussie");
            }
            catch
            {
                MessageBox.Show("Echec");
            }
        }
    }
}
