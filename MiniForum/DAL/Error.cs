using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MiniForum
{
    public class ErrorDAL
    {
        public static void AddNewError(DateTime dtime, string textMessage, string userLogin)
        {
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddErrorMessage", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@addData", dtime);
                    cmd.Parameters.AddWithValue("@errorText", textMessage);
                    if( userLogin !="" )
                        cmd.Parameters.AddWithValue("@userLogin", userLogin);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

    }


}