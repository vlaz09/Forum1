using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniForum
{
    public class SessionManager
    {
        private static string _userID = "UserID";
        private static string _userLogin = "UserLogin";
        private static string _userPass = "UserPass";
        private static string _userName = "UserName";
        private static string _userEmail = "UserEmail";
        private static string _userStatus = "UserStatus";
        private static string _userRole = "UserRole";
        private static string _userConfirmReg = "UserConfirmReg";
       

        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userID] == null)
                { return 0; }
                else
                { return Convert.ToInt32(HttpContext.Current.Session[SessionManager._userID]); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userID] = value; }
        }

        public static string UserLogin
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userLogin] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userLogin].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userLogin] = value; }
        }

        public static string UserPass
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userPass] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userPass].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userPass] = value; }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userName] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userName].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userName] = value; }
        }

        public static string UserEmail
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userRole] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userRole].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userRole] = value; }
        }

        public static string UserStatus
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userStatus] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userStatus].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userStatus] = value; }
        }

        public static string UserRole
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userRole] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session[SessionManager._userRole].ToString(); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userRole] = value; }
        }

        public static bool UserConfirmReg
        {
            get
            {
                if (HttpContext.Current.Session[SessionManager._userConfirmReg] == null)
                { return false; }
                else
                { return Convert.ToBoolean( HttpContext.Current.Session[SessionManager._userConfirmReg] ); }
            }
            set
            { HttpContext.Current.Session[SessionManager._userConfirmReg] = value; }
        }


        public static void SessionAuthUser(Entities.User user)
        {
            SessionManager.UserID = user.ID;
            SessionManager.UserLogin = user.Login;
            SessionManager.UserName = user.Name;
            SessionManager.UserStatus = user.Status;
            SessionManager.UserRole = user.Role;
            SessionManager.UserConfirmReg = user.ConfirmRegistration;
        }

        public static void SessionAuthClear()
        {
            HttpContext.Current.Session.Remove(SessionManager._userID);
            HttpContext.Current.Session.Remove(SessionManager._userLogin);
            HttpContext.Current.Session.Remove(SessionManager._userName);
            HttpContext.Current.Session.Remove(SessionManager._userStatus);
            HttpContext.Current.Session.Remove(SessionManager._userRole);
        }

    }
}