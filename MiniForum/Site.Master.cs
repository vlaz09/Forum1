using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MiniForum
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HeadLoginName();
        }


        private void HeadLoginName()
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ft = ((FormsIdentity)Page.User.Identity).Ticket;

                if (Session["userName"] != null)
                    (HeadLoginView.FindControl("lblUserName") as Label).Text = Session["userName"].ToString();
                else
                {
                    string userLogin = ft.Name;
                    UserDAL userdal = new UserDAL();
                    string userName;
                    try
                    {
                        if (userdal.GetUserName(userLogin, out userName))
                        {
                            (HeadLoginView.FindControl("lblUserName") as Label).Text = userName;
                            Session["userName"] = userName;
                        }
                        else
                            (HeadLoginView.FindControl("lblUserName") as Label).Text = userLogin;

                    }
                    catch (Exception ex)
                    {
                        string text = " Message: " + ex.Message +
                                " StackTrace: " + ex.StackTrace;
                        ErrorDAL.AddNewError(DateTime.Now, text, "");

                        (HeadLoginView.FindControl("lblUserName") as Label).Text = userLogin;
                    }
                }
            }
        }



    }
}
