using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniForum.Account{
    public partial class ConfirmRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string login = Request.Params["login"];
                string sguid = Request.Params["guid"];
                if (login != null && sguid != null)
                {
                    Confirmation(login, sguid);
                }
                else
                { Label3.Visible = true; Label2.Visible = true; }
            }
        }

        private void Confirmation(string login, string sguid)
        {
            Guid guid = new Guid();
            try
            {
                 guid = new Guid(sguid);
            }
            catch (Exception ex)
            {
                Label2.Visible = true;
                Label3.Visible = true;
            }

            string pass;
            UserDAL userdal = new UserDAL();

            if (userdal.IsUserLogin(login, out pass))
            {
                Guid hashpass = AppCode.GetHashEncoding(pass);
                if (hashpass == guid)
                {
                    userdal.UpdateUserConfirmRegistration(login, true);
                    Label1.Visible = true;//запись активирована
                }
                else
                    Label2.Visible = true;//Код активации неправильный
            }
            else
                Label3.Visible = true;//Пользователя с таким логином нет
        
        }

    }
}