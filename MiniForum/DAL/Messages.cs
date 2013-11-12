using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MiniForum
{
    public class Messages
    {
        public int MessagesID { get; private set; }
        public string TopicName { get; private set; }
        public int TopicID { get; private set; }
        public string AutorName { get; private set; }
        public DateTime AddDate { get; private set; }
        public string Text { get; private set; }


        public Messages(int MessagesID, int TopicID, string TopicName, string AutorName, DateTime AddDate, string Text)
        {
            this.MessagesID = MessagesID;
            this.TopicName = TopicName;
            this.TopicID = TopicID;
            this.AutorName = AutorName;
            this.AddDate = AddDate;
            this.Text = Text;
        }
    }


    public class MessagesDAL
    {
        public List<Messages> GetMessagesByTopic(int topicID)
        {
            List<Messages> listMessages = new List<Messages>();

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
                        Messages mess = new Messages(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),
                            reader["Autor"].ToString(),
                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
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

            return listMessages; 
        }

        public List<Messages> GetMessagesByTopic(int topicID, DateTime startDate)
        {
            List<Messages> listMessages = new List<Messages>();

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
                        Messages mess = new Messages(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),
                            reader["Autor"].ToString(),
                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
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

            return listMessages;

        }

        public List<Messages> GetLastMessage(int topicID)
        {
            List<Messages> listMessages = new List<Messages>();
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
                        Messages mess = new Messages(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),
                            reader["Autor"].ToString(),
                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
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
            return listMessages;
        }

        public List<Messages> GetMessageByMessID(int messID)
        {
            List<Messages> listMessages = new List<Messages>();
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
                        Messages mess = new Messages(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["TopicID"]),
                            reader["TopicName"].ToString(),
                            reader["Autor"].ToString(),
                            (DateTime)reader["AddDate"],
                            reader["Text"].ToString()
                            );
                        listMessages.Add(mess);
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
            return listMessages;
        }

        public bool AddNewMessage(int topicID, string userLog, string textMess)
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
                    cmd.Parameters.AddWithValue("@user", userLog);
                    cmd.Parameters.AddWithValue("@addDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@text", textMess);
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

                    con.Dispose();
                }
            }
            catch (Exception e)
            {
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");

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
                    con.Dispose();
                }
            }
            catch (Exception e)
            {
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");

                throw new Exception("Oшибка данных");
            }
        }

    }
}