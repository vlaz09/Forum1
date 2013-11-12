using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

using System.Net.Mail;
using System.Net;
using System.Web;
using System.IO;
using System.Threading;



namespace MiniForum
{
    class AppCode
    {
        public static Guid GetHashEncoding(string s)
        { 
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider md5 =
                new MD5CryptoServiceProvider();
            byte[] byteHash = md5.ComputeHash(bytes);

            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            

            return new Guid(hash);
        }
    }
	
	class Email
    {
        string smtp = "smtp.bk.ru";
        int port = 25;
        string senderEmail = "vlaz09@bk.ru";
        string senderPasword = "m80956157393";

        internal bool SendEmail(string toMail, string subject, string textMassage)
        {
            bool flag = false;
            try
            {
                using (MailMessage mm = new MailMessage())
                {
                    mm.Subject = subject;
                    mm.Body = textMassage;
                    mm.IsBodyHtml = true;

                    mm.From = new System.Net.Mail.MailAddress(senderEmail);//мейл отправителя
                    mm.To.Add(new MailAddress(toMail));//мейл получателя

                    using (SmtpClient sc = new SmtpClient(smtp, port))
                    {
                        //sc.EnableSsl = true; // включение SSL
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.UseDefaultCredentials = false;
                        sc.Timeout = 300;
                        sc.Credentials = new NetworkCredential(senderEmail, senderPasword);
                        
                        sc.Send(mm);

                        flag = true;
                    }
                }
            }

            catch (Exception e)
            {
                string text = " Message: " + e.Message +
                        " StackTrace: " + e.StackTrace;
                ErrorDAL.AddNewError(DateTime.Now, text, "");

                throw new Exception("Oшибка отправки сообщения");
            }

            return flag;
        }
    
    }
}
