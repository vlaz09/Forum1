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
                SwitchParametr( inputparametr);
            }
        }

        private void SwitchParametr(string inputparametr)
        {
            switch (inputparametr)
            {
                case "activateAccount": activateAccount(); break; //Активация аккаунта, отправляется  письмо со ссылкой для активации 
                case "passwordRecovery": ; break;//Восстановление пароля
            }
        }

        //Активация аккаунта
        private void activateAccount()
        {
            if (Session["userLogin"] != null)
            {
                pnlActivateAccount1.Visible = true;
                UserDAL userdal = new UserDAL();
                lblEmail1.Text = Session["userEmail"].ToString();
            }
            // else pnlActivateAccount2.Visible = true;
        }
        //Отправляется письмо на прежний емейл пользователя
        protected void lnkbtnActivateAccount1_Click(object sender, EventArgs e)
        {
            if (Session["userLogin"] != null)
            {
                string login = Session["userLogin"].ToString();
                string pass = Session["userPass"].ToString();
                string userName = Session["userName"].ToString();
                string userEmail = Session["userEmail"].ToString();

                Register registerAspxcs = new Register();
                string htmlTextMess = registerAspxcs.WriteEmailMessage(userName, login, AppCode.GetHashEncoding(pass), Request.Url.Authority);

                Email mail = new Email();
                try
                {
                    bool b = mail.SendEmail(userEmail, "MiniForum - Вы были зарегистрированы", htmlTextMess);
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
            if (Session["userLogin"] != null)
            {
                string login = Session["userLogin"].ToString();
                string pass = Session["userPass"].ToString();
                string userName = Session["userName"].ToString();

                Register registerAspxcs = new Register();
                string htmlTextMess = registerAspxcs.WriteEmailMessage(userName, login, AppCode.GetHashEncoding(pass), Request.Url.Authority);

                UserDAL userdal = new UserDAL();
                Email mail = new Email();
                try
                {
                    if (userdal.UpdateUserMail(login, tbxEmail.Text)) //Сохранение емейла в БД
                    {
                        bool b = mail.SendEmail(tbxEmail.Text, "MiniForum - Вы были зарегистрированы", htmlTextMess);
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