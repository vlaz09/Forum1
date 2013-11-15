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
        internal static string GetHashEncoding(string s)
        { 
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider md5 =
                new MD5CryptoServiceProvider();
            byte[] byteHash = md5.ComputeHash(bytes);

            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
	
	class Mail
    {
        static string smtp = "smtp.bk.ru";
        static int port = 25;
        static string senderEmail = "vlaz09@bk.ru";
        static string senderPasword = "m80956157393";

        internal static bool SendEmail(string toMail, string subject, string textMassage)
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
                        sc.Timeout = 3000;
                        sc.Credentials = new NetworkCredential(senderEmail, senderPasword);
                        
                        sc.Send(mm);
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorDAL.AddNewError(DateTime.Now, e.ToString(), "");
                throw new Exception("Oшибка отправки сообщения");
            }
            return flag;
        }
    
    }
}
