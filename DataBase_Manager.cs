using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Media;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Space_Battleships
{
    public struct UserProperties
    {
        public string UserName;
        public int UserScore; 
    }

    public struct UserData
    {
        public int Points;
        public int Health;
        public float Armor;
        public int Missiles;
        public Boolean Armed_Arms;
        public int Level;
    }


    class DataBase_Manager
    {
        public UserProperties UP = new UserProperties();
        public UserData UD = new UserData();
        
        public DataBase_Manager()
        {

         }


        public void UpDate_SystemDataBase(int Points, int Health, float Armor, int Missiles, Boolean Armed_Arms, int Level)
        {
            try
            {
                SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                Conector1.Open();

                SqlCommand Com1 = new SqlCommand();
                string query1 = "delete from System_DataBase";
                Com1.CommandText = query1;
                Com1.Connection = Conector1;
                Com1.ExecuteNonQuery();

                string query2 = "insert into System_DataBase(Points ,Health, Armor, Missiles, Armed_Arms, Chapter) values(@Points, @Health, @Armor, @Missiles, @Armed_Arms, @Chapter)";
                Com1.CommandText = query2;
                Com1.Connection = Conector1;

                Com1.Parameters.Add("@Points", SqlDbType.NVarChar).Value = Points;
                Com1.Parameters.Add("@Health", SqlDbType.NVarChar).Value = Health;
                Com1.Parameters.Add("@Armor", SqlDbType.NVarChar).Value = Armor;
                Com1.Parameters.Add("@Missiles", SqlDbType.NVarChar).Value = Missiles;
                Com1.Parameters.Add("@Armed_Arms", SqlDbType.NVarChar).Value = Armed_Arms;
                Com1.Parameters.Add("@Chapter", SqlDbType.NVarChar).Value = Level;        //Next level..

                Com1.ExecuteNonQuery();
            }

            catch
            {
                MessageBox.Show("Error: No Sql Connection!!");
            }
        }


        public void Call_SystemDataBase()
        {
            try
            {
                SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                Conector1.Open();

                SqlCommand Com1 = new SqlCommand();
                string query1 = "select * from System_DataBase";

                SqlDataReader mdr;
                Com1 = new SqlCommand(query1, Conector1);
                mdr = Com1.ExecuteReader();

                while (mdr.Read())
                {
                    UD.Points = mdr.GetInt32(0);
                    UD.Health = mdr.GetInt32(1);
                    UD.Armor = (float)mdr.GetDouble(2);
                    UD.Missiles = mdr.GetInt32(3);
                    UD.Armed_Arms = mdr.GetBoolean(4);
                    UD.Level = mdr.GetInt32(5);
                }
            }

            catch
            {
                MessageBox.Show("Error: No Sql Connection!!");
            }
        }


        public void Call_GameDataBase(string UName)
        {
            try
            {
                SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                SqlCommand mcd = new SqlCommand();
                SqlCommand Com1 = new SqlCommand();
                SqlDataReader mdr;

                string Str = "";
                string UScore = "";

                Conector1.Open();
                Str = "SELECT * FROM Game_DataBase WHERE User_Name='" + UName + "'";
                mcd = new SqlCommand(Str, Conector1);
                mdr = mcd.ExecuteReader();

                if (mdr.HasRows)
                {
                    while (mdr.Read())
                    {
                        UP.UserName = mdr[0].ToString();
                        UScore = mdr[1].ToString();
                        UP.UserScore = Convert.ToInt32(UScore);
                    }
                }

                else
                {
                    SqlConnection Conector2 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                    Conector2.Open();
                    SqlCommand Com2 = new SqlCommand();

                    string query1 = "insert into Game_DataBase(User_Name,User_Score) values(@User_Name,@User_Score)";
                    Com2.CommandText = query1;
                    Com2.Connection = Conector2;
                    Com2.Parameters.Add("@User_Name", SqlDbType.NVarChar).Value = UName;
                    Com2.Parameters.Add("@User_Score", SqlDbType.Int).Value = 0;
                    Com2.ExecuteNonQuery();


                    UP.UserName = UName;
                    UP.UserScore = 0;
                }

                Conector1.Close();

            }


            catch (Exception)
            {
                MessageBox.Show("Error: No Sql Connection!!");
            }

        }


        public void Update_GameDataBase(int NewScore, string UName)
        {
            SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
            SqlCommand mcd = new SqlCommand();
            SqlCommand Com1 = new SqlCommand();
            SqlDataReader mdr;

            string Str = "";
            string UScore = "";

            Conector1.Open();
            Str = "SELECT * FROM Game_DataBase WHERE User_Name='" + UName + "'";
            mcd = new SqlCommand(Str, Conector1);
            mdr = mcd.ExecuteReader();

            if (mdr.HasRows)
            {
                while (mdr.Read())
                {
                    UP.UserName = mdr[0].ToString();
                    UScore = mdr[1].ToString();
                    UP.UserScore = Convert.ToInt32(UScore);
                }   
            }

           if (UP.UserScore < NewScore)
           {
                RemoveUser(UName,false);

                SqlConnection Conector2 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                Conector2.Open();
                SqlCommand Com2 = new SqlCommand();

                string query1 = "insert into Game_DataBase(User_Name,User_Score) values(@User_Name,@User_Score)";
                Com2.CommandText = query1;
                Com2.Connection = Conector2;
                Com2.Parameters.Add("@User_Name", SqlDbType.NVarChar).Value = UName;
                Com2.Parameters.Add("@User_Score", SqlDbType.Int).Value = NewScore;
                Com2.ExecuteNonQuery();
                
            }

            Conector1.Close();
            
        }

      
        public void RemoveUser(string UName,Boolean Bol)
        {
            if (IsExist(UName) == false)
            {
               MessageBox.Show("User Not Exist!");
               return;
            }
            
            try
            {
                SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                Conector1.Open();

                SqlCommand Com1 = new SqlCommand();

                string query1 = "delete from Game_DataBase WHERE User_Name='" + UName + "'";
                Com1.CommandText = query1;
                Com1.Connection = Conector1;
                Com1.ExecuteNonQuery();
                Conector1.Close();

                if(Bol)
                MessageBox.Show("User Has Been Deleted.");
            }

            catch
            {
                MessageBox.Show("Error: No Sql Connection!!");
            }
        }

        
        public void ShowHigh_Scores(frmHigh_Scores Frm)
        {
            SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
            string sql = "SELECT * FROM Game_DataBase ORDER BY User_Score desc";

            try
            {
                Conector1.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, Conector1);
                DataSet ds = new DataSet();
                da.Fill(ds, "Game_DataBase");
                DataTable dt = ds.Tables["Game_DataBase"];

                foreach (DataRow row in dt.Rows)
                {
                   Frm.InsertNames(row["User_Name"].ToString());
                   Frm.InsertScores(row["User_Score"].ToString());
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            
            finally
            {
                Conector1.Close();
            }           
        }


        private Boolean IsExist(string UName)
        {
            string Str = null;

            try
            {
                SqlConnection Conector1 = new SqlConnection(@"Data Source=LAPTOP-0MGGVQHC;Initial Catalog=Space-Battleships_DB;Integrated Security=True");
                Conector1.Open();

                SqlCommand Com1 = new SqlCommand();
                string query1 = "select User_Name from Game_DataBase WHERE User_Name='" + UName + "'"; ;

                SqlDataReader mdr;
                Com1 = new SqlCommand(query1, Conector1);
                mdr = Com1.ExecuteReader();

                while (mdr.Read())
                {
                    Str = mdr[0].ToString();
                }

                if (Str != null)
                    return true;
            }

            catch
            {
                MessageBox.Show("Error: No Sql Connection!!");
            }

            return false;
        }


    }


}


       
    

