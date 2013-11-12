using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MiniForum
{

    //Topic.ID, Topic.Name, Topic.Autor, Topic.AddDate, 
    //CountMess.countReply, 
    //LastMess.ID AS IDLastMess, LastMess.Autor AS AutorLastMess,LastMess.AddDate AS DataLastMess


    public class Topics
    {
        public int TopicID { get; private set; }
        public string TopicName { get; private set; }
        public string TopicAutor { get; private set; }
        public DateTime TopicAddDate { get; private set; }

        public int CountReply { get; private set; }
        public int? LastMessID { get; private set; }
        public string LastMessAutor { get; private set; }
        public DateTime? LastMessData { get; private set; }

        public Topics(int TopicID, string TopicName, string TopicAutor, DateTime TopicAddDate, int CountReply, int? LastMessID, string LastMessAutor, DateTime? LastMessData)
        {
            this.TopicID = TopicID;
            this.TopicName = TopicName;
            this.TopicAutor = TopicAutor;
            this.TopicAddDate = TopicAddDate;

            this.CountReply = CountReply;
            this.LastMessID = LastMessID;
            this.LastMessAutor = LastMessAutor;
            this.LastMessData = LastMessData;
        }

    }

    public class TopicDAL
    {
        public List<Topics> GetForumTopic(int sectionID)
        {
            List<Topics> listTopic = new List<Topics>();

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetTopic", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("sectionID", sectionID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //проверка входных данных на DBNull.Value
                        int? idLastMess = null;
                        if (reader["IDLastMess"] != DBNull.Value)
                            idLastMess = Convert.ToInt32(reader["IDLastMess"]);

                        DateTime? dataLastMess = null;
                        if (reader["DataLastMess"] != DBNull.Value)
                            dataLastMess = (DateTime)reader["DataLastMess"];

                        Topics topic = new Topics(
                                Convert.ToInt32(reader["ID"]),
                                reader["Name"].ToString(),
                                reader["Autor"].ToString(),
                                (DateTime)reader["AddDate"],
                                (reader["countReply"] != DBNull.Value) ? Convert.ToInt32(reader["countReply"]) : 0,
                                idLastMess, //(reader["IDLastMess"] != DBNull.Value)?Convert.ToInt32(reader["IDLastMess"]):null,
                                (reader["AutorLastMess"] != DBNull.Value) ? reader["AutorLastMess"].ToString() : "",
                                dataLastMess
                            );

                        listTopic.Add(topic);
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
            return listTopic;
        }

        public int AddNewTopic(string userLog, string text, int sectionID)
        {
            int resultTopicID = 0;
            bool flag = false;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("AddNewTopic", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@sectionID", sectionID);
                    cmd.Parameters.AddWithValue("@autor", userLog);
                    cmd.Parameters.AddWithValue("@name", text);
                    cmd.Parameters.AddWithValue("@addDate", dtime);

                    con.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                        flag = true;


                    if (flag)
                    {
                        SqlCommand cmd2 = new SqlCommand("GetIDNewTopic", con);
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@autor", userLog);
                        cmd2.Parameters.AddWithValue("@addDate", dtime);

                        SqlDataReader reader = cmd2.ExecuteReader();

                        while (reader.Read())
                        {

                            reader["ID"].ToString();

                            if (reader["ID"] != DBNull.Value)
                                resultTopicID = Convert.ToInt32(reader["ID"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string textError = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, textError, "");

                throw new Exception("Oшибка данных");
            }
            return resultTopicID;
        }

        public string GetTopicName(int topicID)
        {
            string topicName = "";

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("GetTopicName", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topicID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        topicName = reader["TopicName"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                string textError = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, textError, "");

                throw new Exception("Oшибка данных");
            }
            return topicName;  
        }

        public void DeleteTopic(int topID)
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteTopic", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                string textError = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, textError, "");

                throw new Exception("Oшибка данных");
            }
        }

        public void UpdateTopic(int topID, string textName)
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateTopicName", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@topicID", topID);
                    cmd.Parameters.AddWithValue("@topicName", textName);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                string textError = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, textError, "");

                throw new Exception("Oшибка данных");
            }
        }
    }
}