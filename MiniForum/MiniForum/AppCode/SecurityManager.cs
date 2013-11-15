using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;


namespace MiniForum
{
    public class SecurityManager
    {


        public static bool GetCurrentUser(HttpRequest reguest)
        {
            bool b = SessionManager.UserConfirmReg;
            if ((SessionManager.UserID != 0) && SessionManager.UserConfirmReg)//Сессия у пользователя жива
            {
                return true;
            }
            else
            {
                //Восстанавливаем авторизацию из куков
                 return RebuildAuthOfCookie(reguest);
            } 
        }

        //Восстановление авторизацию из куков. если удачно вернет true
        private static bool RebuildAuthOfCookie(HttpRequest reguest)
        {
            HttpCookie cookie = reguest.Cookies["MiniForumCookieName"];
            if (cookie != null)
            {
                System.Web.Security.FormsAuthenticationTicket authTicket = System.Web.Security.FormsAuthentication.Decrypt(cookie.Value);
                int userID; //ИД пользователя из куков 
                bool tryParseIdToInt = Int32.TryParse(authTicket.Name, out userID);
                if (tryParseIdToInt ) 
                {
                    UserDAL userdal = new UserDAL();
                    Entities.User users = userdal.UserAuthenticationDB(userID);

                    if (users != null)
                    {
                        SessionManager.SessionAuthUser(users);
                        return true;
                    }
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        


    }
}