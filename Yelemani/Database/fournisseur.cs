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
    class fournisseur
    {
        MySqlConnection con;
        MySqlCommand cmd;


        public fournisseur()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root; password=;database=yelemani;");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root; password=;database=yelemani;");

            con.Close();
        }

        public DataSet refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Telephone,Lieu,Solde,Date_de_modification, Modificateur from fournisseur", con);
            DataSet ds = new DataSet();

            da.Fill(ds);
            con.Close();
            return ds;
        }

        public void add(string nom, int telephone, int solde, string modificateur, string date,string prenom, string lieu)
        {
            con.Open();
            string NomPrenom = nom + ' ' + prenom;
            cmd = new MySqlCommand("insert into fournisseur(nom, telephone, solde, date_de_modification,modificateur, lieu) Values ('" + NomPrenom + "','" + telephone + "','" + solde + "','" + date + "','" + modificateur + "', '" + lieu + "')", con);
            // con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void delete(string nom, string telephone)
        {
            cmd = new MySqlCommand("delete from fournisseur WHERE nom='" + nom + "' AND telephone='" + telephone + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void update(string nom, string telephone,string lieu, string solde, string selected1, string selected2, string modificateur, string date)
        {
            //string NomPrenom = nom + ' ' + prenom;
            cmd = new MySqlCommand("update fournisseur set nom='" + nom + "',  telephone='" + telephone + "',lieu='" + lieu + "',solde='" + Convert.ToDouble(solde) + "', Date_de_modification='" + date + "', modificateur='" + modificateur + "'  where nom='" + selected1 + "' and telephone='" + selected2 + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
