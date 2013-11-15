using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace MiniForum.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "RegisterDB.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void BntLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            UserDAL userdal = new UserDAL();
            Entities.User users = null;

            string login = txtbxLoginUser.Text.Trim();

            string pass = AppCode.GetHashEncoding(txtbxPassword.Text.Trim());//хеш пароля

            try
            {
                users = userdal.UserAuthenticationDB(login, pass);
                if ( users != null )
                {
                    if (users.ConfirmRegistration)  //Пользователь зарегистрирован, учетная запись активирована 
                    {
                        FormsAuthentication.SetAuthCookie( users.ID.ToString(), false );//Добавление логина в куки
                        SessionManager.SessionAuthUser(users);//Добавление аутентификации пользователя в сессию
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false), false);
                    }
                    else//Пользователь зарегистрирован, но не активировал учетную запись
                    {
                        SessionManager.UserID = users.ID;
                        SessionManager.UserLogin = users.Login;
                        SessionManager.UserPass = users.Pass;
                        SessionManager.UserName = users.Name;
                        SessionManager.UserEmail = users.Email;

                        pnlRegistration.Visible = false;
                        pnlReactivate.Visible = true;
                        return;
                    }
                }
                else
                {
                    ErrorMassage.Text = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            { ErrorMassage.Text = ex.Message; }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Account/ReactivateAccount.aspx?reactivate=activateAccount");
        }
    }
}
