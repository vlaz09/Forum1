using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniForum.Account
{
    public partial class ReactivateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string inputparametr = Request.Params["reactivate"];
                SwitchParamUrlStr(inputparametr);
            }
        }

        private void SwitchParamUrlStr(string inputparametr)//параметр из строки запроса
        {
            switch (inputparametr)
            {
                case "activateAccount": activateAccount(); break; //Активация аккаунта, отправляется  письмо со ссылкой для активации 
                //TODO //case "passwordRecovery": ; break;//Восстановление пароля
            }
        }

        //Активация аккаунта
        private void activateAccount()
        {
            if (SessionManager.UserID != 0)
            {
                pnlActivateAccount1.Visible = true;
                UserDAL userdal = new UserDAL();
                lblEmail1.Text = SessionManager.UserEmail.ToString();
            }
        }
        //Отправляется письмо на прежний емейл пользователя
        protected void lnkbtnActivateAccount1_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                string login = SessionManager.UserLogin;
                string pass = SessionManager.UserPass;
                string userName = SessionManager.UserName;
                string userEmail = SessionManager.UserEmail;

                RegisterDB registerAspxcs = new RegisterDB();// CodeBehind страницы регистрации RegisterDB.Aspx.cs
                string htmlTextMess = registerAspxcs.WriteEmailMessage(userName, login, AppCode.GetHashEncoding(pass), Request.Url.Authority);//Метод для генерации сообщения с кодом активации

                try
                {
                    bool b = Mail.SendEmail(userEmail, "MiniForum - Вы были зарегистрированы", htmlTextMess);
                    Panel2.Visible = true;
                    pnlActivateAccount1.Visible = false;
                    Session.Clear();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }
        }

        //Отправляется письмо на введенный емейл
        protected void btnActivateAccount1_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                string login = SessionManager.UserLogin;
                string pass = SessionManager.UserPass;
                string userName = SessionManager.UserName;

                RegisterDB registerAspxcs = new RegisterDB();
                string htmlTextMess = registerAspxcs.WriteEmailMessage(userName, login, AppCode.GetHashEncoding(pass), Request.Url.Authority);

                UserDAL userdal = new UserDAL();
                
                try
                {
                    if (userdal.UpdateUserMail(SessionManager.UserID, tbxEmail.Text)) //Сохранение емейла в БД
                    {
                        bool b = Mail.SendEmail(tbxEmail.Text, "MiniForum - Вы были зарегистрированы", htmlTextMess);
                        Panel2.Visible = true;
                        pnlActivateAccount1.Visible = false;
                        Session.Clear();
                    }
                    else
                    {
                        ErrorMessage.Text = "При сохранении емейла возникла ошибка.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }

        }

        //Восстановление пароля


    }
}