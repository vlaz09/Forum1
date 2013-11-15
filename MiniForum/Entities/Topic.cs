using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Topic
    {
        public int TopicID { get; private set; }
        public string TopicName { get; private set; }
        public int UserID { get; private set; }
        public string UserLogin { get; private set; }
        public DateTime AddDate { get; private set; }

        public int CountReply { get; private set; }
        public int? LastMessID { get; private set; }

        public int? UserIDLastMess { get; private set; }
        public string UserLoginLastMess { get; private set; }

        public DateTime? LastMessData { get; private set; }

        public Topic(int TopicID, string TopicName, int UserID, string UserLogin, DateTime AddDate, int CountReply, int? LastMessID, int? UserIDLastMess, string UserLoginLastMess, DateTime? LastMessData)
        {
            this.TopicID = TopicID;
            this.TopicName = TopicName;
            this.UserID = UserID;
            this.UserLogin = UserLogin;
            this.AddDate = AddDate;

            this.CountReply = CountReply;
            this.LastMessID = LastMessID;

            this.UserIDLastMess = UserIDLastMess;
            this.UserLoginLastMess = UserLoginLastMess;

            this.LastMessData = LastMessData;
        }
    }
}
