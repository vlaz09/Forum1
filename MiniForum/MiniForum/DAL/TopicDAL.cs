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




    public class TopicDAL
    {
        public List<Entities.Topic> GetTopics(int sectionID)
        {
            List<Entities.Topic> listTopic = new List<Entities.Topic>();

            try
            {
                using (SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetTopics", con);
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

                        int? userIDLastMess = null;
                        if (reader["IDLastMess"] != DBNull.Value)
                            userIDLastMess = Convert.ToInt32(reader["IDLastMess"]);


                        DateTime? dataLastMess = null;
                        if (reader["DataLastMess"] != DBNull.Value)
                            dataLastMess = (DateTime)reader["DataLastMess"];

                        Entities.Topic topic = new Entities.Topic(
                                Convert.ToInt32(reader["ID"]),
                                reader["Name"].ToString(),
                                Convert.ToInt32(reader["UserID"]),
                                reader["Login"].ToString(),

                                (DateTime)reader["AddDate"],
                                (reader["countReply"] != DBNull.Value) ? Convert.ToInt32(reader["countReply"]) : 0,
                                idLastMess, //(reader["IDLastMess"] != DBNull.Value)?Convert.ToInt32(reader["IDLastMess"]):null,
                                userIDLastMess,
                                (reader["UserLoginLastMess"] != DBNull.Value) ? reader["UserLoginLastMess"].ToString() : "",
                                dataLastMess
                            );

                        listTopic.Add(topic);
                    }
                }
            }

            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
            return listTopic;
        }

        public int AddNewTopic(int userID, string text, int sectionID)
        {
            int resultTopicID = 0;
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

                    cmd.Parameters.AddWithValue("@userID", userID);

                    cmd.Parameters.AddWithValue("@name", text);
                    cmd.Parameters.AddWithValue("@addDate", dtime);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["TopicID"] != DBNull.Value)
                            resultTopicID = Convert.ToInt32(reader["TopicID"]);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
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
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
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
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
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
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка данных");
            }
        }
    }
}