using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Message
    {
        public int MessagesID { get; private set; }
        public string TopicName { get; private set; }
        public int TopicID { get; private set; }

        public int UserID { get; private set; }
        public string UserLogin { get; private set; }

        public DateTime AddDate { get; private set; }
        public string Text { get; private set; }


        public Message(int MessagesID, int TopicID, string TopicName, int UserID, string UserLogin, DateTime AddDate, string Text)
        {
            this.MessagesID = MessagesID;
            this.TopicID = TopicID;
            this.TopicName = TopicName;
            this.UserID = UserID;
            this.UserLogin = UserLogin;
            this.AddDate = AddDate;
            this.Text = Text;
        }
    }
}
