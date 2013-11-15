using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Configuration;

namespace MiniForum
{
    public class UserDAL
    {
        public Entities.User UserAuthenticationDB(string userlogin, string userPassword)
        {
            Entities.User user = null;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("GetUserInfo", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@usr", userlogin);
                    cmd.Parameters.AddWithValue("@pwd", userPassword);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user = new Entities.User(
                            Convert.ToInt32(dr["ID"]),
                            userlogin,
                            userPassword,
                            dr["UserName"].ToString(),
                            dr["Email"].ToString(),
                            dr["Status"].ToString(),
                            dr["RoleName"].ToString().Trim(),
                            Convert.ToBoolean(dr["ConfirmRegistration"])
                       );
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return user;
        }


        public Entities.User UserAuthenticationDB(int userID)
        {
            Entities.User user = null;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetUserAuth", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userID", userID);
                    
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user = new Entities.User(
                            userID,
                            dr["Login"].ToString(),
                            dr["UserName"].ToString(),
                            dr["Status"].ToString(),
                            dr["RoleName"].ToString().Trim(),
                            Convert.ToBoolean(dr["ConfirmRegistration"])
                        );
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return user;
        }


        public void UserAuthenticationDB2(int userID, out string user)
        {
             user = null;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetUserAuth", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userID", userID);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                       user = userID +"; "+
                        dr["Login"].ToString() + "; " +
                        dr["UserName"].ToString() + "; " +
                        dr["Status"].ToString() + "; " +
                        dr["RoleName"].ToString().Trim();

                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
           
        }

        //public bool GetUserName(string userlogin, out string userName)
        //{
        //    userName = string.Empty;
           
        //    bool flag = false;
        //    try
        //    {
        //        using (SqlConnection con =
        //            new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
        //        {

        //            SqlCommand cmd = new SqlCommand("GetUserName", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@usr", userlogin);
                    
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            userlogin = "";
        //            while (dr.Read())
        //            {
        //                userName = dr["UserName"].ToString();
        //                flag = true;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string text = " Message: " + e.Message +
        //                " StackTrace: " + e.StackTrace;
        //        ErrorDAL.AddNewError(DateTime.Now, text, "");

        //        throw new Exception("Oшибка данных");
        //    }
        //    return flag;
        //}

        /*
        /// <summary>
        /// Если существует пользователь с таким логином, вернет true
        /// </summary>
        /// <returns></returns>
        public bool IsUserLogin(string userlogin)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("IsUserLogin", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", userlogin);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        flag = true;
                }
            }
            catch (Exception e)
            {
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");

                throw new Exception("Oшибка данных");
            }

            return flag;
        }
        */

        //IsUserLogin
        public string GetUserPass(string userlogin)
        {
            string password = string.Empty;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetUserPass", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", userlogin);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        password = reader["Password"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return password;
        }

        //Если есть логин или емейл в БД вернет true
        public bool IsUserLoginEmail(string userLogin, string userEmail)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("IsUserLoginEmail", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", userLogin);
                    cmd.Parameters.AddWithValue("@email", userEmail);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if ((reader["Login"].ToString().Trim() == userLogin) || (reader["Email"].ToString().Trim() == userEmail))
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return flag;
        }

        public bool AddNewUser(string login, string password, string userName, string email)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddNewUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@dataRegistration", DateTime.Now);
                    con.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                        flag = true;
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка записи пользовательских данных");
            }
            return flag;
        }

        public bool UpdateUserMail(int userID, string email)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateUserEmail", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@email", email);

                    con.Open();
                    if(cmd.ExecuteNonQuery() == 1)
                        flag = true;
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return flag;
        }

        public bool UpdateUserConfirmRegistration(string login, bool isConfirmRegistration)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateUserConfirmRegistration", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@confirmRegistration", isConfirmRegistration);

                    con.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                        flag = true;
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return flag;
        }


    }
}