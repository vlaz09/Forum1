using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;

namespace MiniForum
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }







        protected void Button1_Click(object sender, EventArgs e)
        {


            StringBuilder htmlString = new StringBuilder();
            if (Request.IsAuthenticated)
            {
                // Display generic identity information.
                // This is always available, regardless of the type of
                // authentication.
                htmlString.Append("<h3>Generic User Information</h3>");
                htmlString.Append("<b>Name: </b>");
                htmlString.Append(User.Identity.Name);
                htmlString.Append("<br><b>Authenticated With: </b>");
                htmlString.Append(User.Identity.AuthenticationType);
                htmlString.Append("<br><br>");

                htmlString.Append(Roles.GetRolesForUser(User.Identity.Name)[0]);
                // Was forms authentication used?
                if (User.Identity is FormsIdentity)
                {
                    // Get the ticket.
                    FormsAuthenticationTicket ticket =
                     ((FormsIdentity)User.Identity).Ticket;

                    htmlString.Append("<h3>Ticket User Information</h3>");
                    htmlString.Append("<b>Name: </b>");
                    htmlString.Append(ticket.Name);
                    htmlString.Append("<br><b>Issued at: </b>");
                    htmlString.Append(ticket.IssueDate);
                    htmlString.Append("<br><b>Expires at: </b>");
                    htmlString.Append(ticket.Expiration);
                    htmlString.Append("<br><b>Cookie version: </b>");
                    htmlString.Append(ticket.Version);
                }
                // Display the information.
                Label1.Text = htmlString.ToString();
            } 
            

            
        }




       
    }
}
