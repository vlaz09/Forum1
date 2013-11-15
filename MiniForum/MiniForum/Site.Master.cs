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
            if (!Page.IsPostBack)
            {
                if (SecurityManager.GetCurrentUser(Request))
                {
                    lblUserName.Text = SessionManager.UserName;
                    pnlLoggedIn.Visible = true;
                    pnlAnonymous.Visible = false;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SessionManager.SessionAuthClear();
            if (Request.Cookies["MiniForumCookieName"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MiniForumCookieName");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            pnlLoggedIn.Visible = false;
            pnlAnonymous.Visible = true;
        }


       



    }
}
