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

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            UserDAL users = new UserDAL();
            string login = LoginUser.UserName.Trim();

            Guid pass = AppCode.GetHashEncoding(LoginUser.Password.Trim());//хеш пароля
            string userName;
            string userRole;
            bool confirmRegistration;
            string email;

            try
            {
                if (users.UserAuthenticationDB(login, pass, out userName, out userRole, out confirmRegistration, out email))
                {
                    if (!confirmRegistration)//Пользователь зарегистрирован, но не активировал учетную запись
                    {
                        Session["userLogin"] = login;
                        Session["userPass"] = pass;
                        Session["userName"] = userName;
                        Session["userEmail"] = email;
                        pnlRegistration.Visible = false;
                        pnlReactivate.Visible = true;
                        return;
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(login, false);
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false), false);
                        Session["userName"] = userName;
                        // Добавление пользователю роль
                        if (Roles.RoleExists(userRole))
                            if (!Roles.IsUserInRole(login, userRole))
                                Roles.AddUserToRole(login, userRole);
                            else return;
                        else
                        {
                            Roles.CreateRole(userRole);
                            Roles.AddUserToRole(login, userRole);
                        }
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
