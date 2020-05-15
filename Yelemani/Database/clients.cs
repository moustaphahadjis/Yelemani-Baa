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
    class clients
    {
        MySqlConnection con;
        MySqlCommand cmd;
        

        public clients ()
        {
            //con = new MySqlConnection("datasource=169.254.49.200;port=3306;username=root; password=;database=yelemani;");
            con = new MySqlConnection("datasource=localhost;port=3306;username=root; password=;database=yelemani;");

            con.Close();
        }

        public DataSet refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Telephone,Solde,Credit,Lieu,Date_de_modification, Modificateur  from clients", con);
            DataSet ds = new DataSet();
            
            da.Fill(ds);
            con.Close();
            return ds;
        }

        public void add(string nom,string prenom, string telephone, double solde, string modificateur, string date, string lieu, double credit)
        {
            con.Open();
            string NomPrenom = nom + ' ' + prenom;
            cmd = new MySqlCommand("insert into clients(nom, telephone, solde, date_de_modification,modificateur,credit,lieu) Values ('" + NomPrenom + "','" + telephone + "','" + solde + "','" + date + "','" + modificateur + "','"+credit+"','"+lieu+"')", con);
           // con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void add(string nom, string telephone, string modificateur)
        {
            con.Open();

            cmd = new MySqlCommand("insert into clients(nom, telephone, solde, date_de_modification,modificateur,lieu,prenom,credit) Values ('" + nom + "','" + telephone + "','" + 0 + "','" + DateTime.Now.ToString() + "','" + modificateur + "','Neant',' ', '0')", con);
            // con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void delete(string nom, string telephone)
        {
            cmd = new MySqlCommand("delete from clients WHERE nom='" + nom + "' AND telephone='" + telephone + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void update(string nom, string telephone, string solde, string selectedname, string selectedtelephone, string modificateur, string date, string credit,string lieu)
        {
            cmd = new MySqlCommand("update clients set nom='" + nom + "', telephone='" + telephone + "', solde='" + Convert.ToDouble(solde) + "', Date_de_modification='" + date + "', modificateur='" + modificateur + "',credit='" + credit + "', lieu='"+lieu+"' where nom='" + selectedname + "' and telephone='" + selectedtelephone + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
