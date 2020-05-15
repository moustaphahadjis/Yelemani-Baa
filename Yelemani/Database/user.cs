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
    class user
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public user()
        {
            try
            {
                con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yelemani;");

                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public DataTable checkuser(string str1, string str2)
        {
            con.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Admin,Rappel from user where username='" + str1 + "' and password='" + str2 + "'",con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            return dt;
        }

        public bool chechusername(string str1)
        {
            con.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("select nom from user where username='" + str1+"'" , con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            if (dt.Rows.Count != 0)
                return true;
            else
                return false;

        }

        public void add(string str1, string str2, string str3, string str4, string str5, string str6, string str7)
        {
            cmd = new MySqlCommand("insert into user (nom, prenom, username, password, rappel, admin,telephone) values('" + str1.ToUpper() + "','" + str2.ToUpper() + "','" + str3.ToUpper() + "','" + str4 + "','" + str6 + "','" + str5.ToUpper() + "','" + str7.Trim() + "')", con);
            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void clear(string str1)
        {
            cmd = new MySqlCommand("delete from user where username='" + str1.ToUpper() + "'", con);
            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }

        public DataTable refresh()
        {
            con.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Prenom,Telephone,Username,Password,Rappel,Admin from user ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            return dt;
        }
        public DataTable refresh(string username, string password)
        {
            con.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("select Nom,Prenom,Telephone,Username,Password,Rappel,Admin from user where username='"+username+"' and password='"+password+"' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            return dt;
        }
        public void update(string nom, string prenom, string telephone, string username, string password,string rappel,string statut,string selectedName, string selectedtelephone)
        {
            cmd = new MySqlCommand("update user set nom='" + nom + "',prenom='"+prenom+"',telephone='"+telephone+ "',username='" + username + "',password='" + password + "',rappel='" + rappel + "',admin='"+statut+"'  where nom='" + selectedName+"' and telephone='"+selectedtelephone+"'", con);
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void update(string nom, string prenom, string telephone, string username, string password, string rappel, string selectedUser, string selectedpassword)
        {
            try
            {
                cmd = new MySqlCommand("update user set nom='" + nom + "',prenom='" + prenom + "',telephone='" + telephone + "',username='" + username + "',password='" + password + "',rappel='" + rappel + "'  where username='" + selectedUser + "' and password='" + selectedpassword + "'", con);
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Compte mis à jour avec succès");
            }
            catch
            {
                MessageBox.Show("Echec");
            }
        }
        public string getrappel(string str)
        {
            con.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("select rappel from user where username='"+str+"'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            if (dt.Rows.Count != 0)
                return dt.Rows[0].ItemArray[0].ToString();
            else
                return null;
        }
    }
}
