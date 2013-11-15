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
                string login = Request.Params["login"].Trim();
                string identityCode = Request.Params["guid"].Trim();
                if (login != "" && identityCode != "")
                {
                    Confirmation(login, identityCode);
                }
                else
                { Label3.Visible = true; Label2.Visible = true; }
            }
        }

        private void Confirmation(string login, string identityCode)
        {
            UserDAL userdal = new UserDAL();
            string pass = userdal.GetUserPass(login);
            if (pass != string.Empty )
            {
                string hashpass = AppCode.GetHashEncoding(pass);
                if ( hashpass.Trim() == identityCode )
                {
                    userdal.UpdateUserConfirmRegistration(login, true);
                    Label1.Visible = true;//запись активирована
                }
                else
                    Label2.Visible = true;
            }
            else
                Label2.Visible = true; //Код активации неправильный
        
        }

    }
}