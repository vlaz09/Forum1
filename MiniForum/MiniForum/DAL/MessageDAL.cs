using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace MiniForum
{
    


    public class MessageDAL
    {
        public List<Entities.Message> GetMessagesByTopic(int topicID)
        {
            List<Entities.Message> listMessages = new List<Entities.Message>();

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetMessages", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topicID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Message mess = new Entities.Message(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),
                            
                            Convert.ToInt32(reader["UserID"]),
                            reader["UserLogin"].ToString(),

                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return listMessages; 
        }

        public List<Entities.Message> GetMessagesByTopic(int topicID, DateTime startDate)
        {
            List<Entities.Message> listMessages = new List<Entities.Message>();

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetMessagesbyTopAndDate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topicID);
                    cmd.Parameters.AddWithValue("@data", startDate);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Message mess = new Entities.Message(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),

                            Convert.ToInt32(reader["UserID"]),
                            reader["UserLogin"].ToString(),

                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
                    }

                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return listMessages;
        }

        public List<Entities.Message> GetLastMessage(int topicID)
        {
            List<Entities.Message> listMessages = new List<Entities.Message>();
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetLastMess", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topicID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Message mess = new Entities.Message(
                             Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),

                            Convert.ToInt32(reader["UserID"]),
                            reader["UserLogin"].ToString(),

                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
                    }
                    
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return listMessages;
        }

        public List<Entities.Message> GetMessageByMessID(int messID)
        {
            List<Entities.Message> listMessages = new List<Entities.Message>();
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetMessageByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@messID", messID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Message mess = new Entities.Message(
                             Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),

                            Convert.ToInt32(reader["UserID"]),
                            reader["UserLogin"].ToString(),

                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return listMessages;
        }

        public bool AddNewMessage(int topicID, int userID, string textMess)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("AddNewMess", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@topID", topicID);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@addDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@text", textMess);
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

        public void DeleteMessage(int messID)
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("DeleteMessage", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@messID", messID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
        }

        public void UpdateMessage(int messID, string messText)
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateMessText", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@messID", messID);
                    cmd.Parameters.AddWithValue("@messText", messText);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
        }

    }
}