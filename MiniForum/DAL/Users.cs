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
    public class Users
    {
        public string Login { get; private set; }
        public string Pass { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DataRegistr { get; private set; }
        public string Status { get; private set; }
        public string Role { get; private set; }
        public bool ConfirmRegistration{ get; private set; }

        public Users (string Name, string Role, bool ConfirmRegistration)
        {
            this.Name = Name;
            this.Role = Role;
            this.ConfirmRegistration = ConfirmRegistration;
        }
    
    }


    public class UserDAL
    {
        string connectionString = @"Data Source=(local)\SQLEXPRESS; AttachDbFileName=|DataDirectory|DBMiniForum.mdf; Integrated Security=True; User Instance=True";

        public bool UserAuthenticationDB(string userlogin, Guid userPassword, out string userName, out string userRole, out bool confirmRegistration, out string email)
        {
            userName = string.Empty;
            userRole = string.Empty;
            confirmRegistration = false;
            email = string.Empty;
            bool flag = false;

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

                    userlogin = "";
                    while (dr.Read())
                    {
                        userName = dr["UserName"].ToString();
                        userRole = dr["RoleName"].ToString();
                        confirmRegistration = Convert.ToBoolean(dr["ConfirmRegistration"]);
                        email = dr["Email"].ToString();
                        flag = true;
                    }
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


        public bool GetUserName(string userlogin, out string userName)
        {
            userName = string.Empty;
           
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("GetUserName", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usr", userlogin);
                    
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    userlogin = "";
                    while (dr.Read())
                    {
                        userName = dr["UserName"].ToString();
                        flag = true;
                    }
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


        /// <summary>
        /// Если существует пользователь с таким логином, вернет true
        /// </summary>
        /// <returns></returns>
        //public bool IsUserLogin(string userlogin)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        using (SqlConnection con =
        //            new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
        //        {

        //            SqlCommand cmd = new SqlCommand("IsUserLogin", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@login", userlogin);

        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            if (dr.HasRows)
        //                flag = true;
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

        public bool IsUserLogin(string userlogin, out string password)
        {
            bool flag = false;
            password = "";
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("IsUserLogin", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", userlogin);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        password = reader["Password"].ToString();
                        flag = true;
                    }
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

        public bool[] IsUserLoginEmail(string userLogin, string userEmail)
        {
            bool[] flag = new bool[2]{false,false};
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
                        if (reader["Login"].ToString().Trim() == userLogin) flag[0] = true;
                        if (reader["Email"].ToString().Trim() == userEmail) flag[1] = true;
                    }
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

        public bool AddNewUser(string login, Guid password, string userName, string email)
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
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");
                throw new Exception("Oшибка записи пользовательских данных");
            }

            return flag;
        }

        public bool UpdateUserMail(string login, string email)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateUserEmail", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@email", email);

                    con.Open();
                    if(cmd.ExecuteNonQuery() == 1)
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
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");

                throw new Exception("Oшибка данных");
            }
            return flag;
        }


    }
}