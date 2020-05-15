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
    class comptabilite
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public comptabilite()
        {
            con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani");

            con.Close();
        }

        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select Nom_Produit, Prix, Quantite, Date from historique", con);
            DataTable ds = new DataTable();

            da.Fill(ds);

            con.Close();
            return ds;
        }
    }
}
