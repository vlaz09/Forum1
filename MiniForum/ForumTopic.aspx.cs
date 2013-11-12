using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MiniForum
{
    public partial class ForumTopic : System.Web.UI.Page
    {
        int categoryid = 1;//раздел темы,( = 1 заглушка пока разделы не созданы)
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificationRequestParams();

            if (!IsPostBack)
            {
                dataFillingRptr(categoryid);
            }
        }

        //Получает параметр categoryid из строки адреса
        private void VerificationRequestParams()
        {
            if (Page.RouteData.Values["categoryid"] != null) //если url вида: {categoryid}/ForumTopic
            {
                try
                {
                    categoryid = Convert.ToInt32( Page.RouteData.Values["categoryid"] );
                }
                catch (Exception ex)
                {
                    string text = " Message: " + ex.Message +
                            " StackTrace: " + ex.StackTrace;
                    ErrorDAL.AddNewError(DateTime.Now, text, "");

                    pnlAddNewTopic.Visible = false;
                    ErrorMessage.Text = "У вас нет разрешения на доступ к этой странице!";
                }
            }
            else
                if (Request.Params["categoryid"] != null)//если url вида: ForumTopic.aspx?categoryid = 
                {
                    try
                    {
                        categoryid = Convert.ToInt32( Request.Params["categoryid"] );
                    }
                    catch (Exception ex)
                    {
                        string text = " Message: " + ex.Message +
                                " StackTrace: " + ex.StackTrace;
                        ErrorDAL.AddNewError(DateTime.Now, text, "");
                        pnlAddNewTopic.Visible = false;
                        ErrorMessage.Text = "У вас нет разрешения на доступ к этой странице!";
                    }
                }
        }

        //привязка репитора к данным 
        private void dataFillingRptr(int ID)
        {
            TopicDAL fTop = new TopicDAL();

            try
            {
                List<Topics> listTopic = fTop.GetForumTopic(categoryid);

                RptrTopic.DataSource = listTopic;
                RptrTopic.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                pnlAddNewTopic.Visible = false;//Панель для создания новой темы
            }
        }

        //создает тему
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                string text = TextBox1.Text;
                TopicDAL top = new TopicDAL();
                try
                {
                    int topicid = top.AddNewTopic(User.Identity.Name, text, 1);
                    HttpContext.Current.Response.Redirect(String.Format("~/ForumMessage.aspx?topicid={0}", topicid));
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }

            else FormsAuthentication.RedirectToLoginPage();
        }

        protected void RptrTopic_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            #region вкл.отображ. панели для переименования темы 
            if (e.CommandName == "Rename")
            {
                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null) 
                    pnl1.Visible = true;

                HyperLink hprlnk = e.Item.FindControl("hprlnkTopic") as HyperLink;
                if(hprlnk != null)
                    hprlnk.Visible = false;
            }
            #endregion

            #region Удалить тему
            if (e.CommandName == "Delete")
            { 
                TopicDAL fTop = new TopicDAL();
                
                if (e.CommandArgument != null)
                try
                {
                        fTop.DeleteTopic(Convert.ToInt32(e.CommandArgument));
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }
            #endregion

            #region переименов. названия темы
            if (e.CommandName == "SaveReName")
            {

                TextBox txtbx1 = e.Item.FindControl("txtbxTopicName") as TextBox;
                    if(txtbx1 != null) 
                    {
                        TopicDAL fTop = new TopicDAL();
                        try
                        {
                            string topName = txtbx1.Text.ToString();

                            if (e.CommandArgument != null)
                                fTop.UpdateTopic(Convert.ToInt32(e.CommandArgument), topName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage.Text = ex.Message;
                        }
                    }

                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null)
                    pnl1.Visible = false;

                HyperLink hprlnk = e.Item.FindControl("hprlnkTopic") as HyperLink;
                if (hprlnk != null)
                    hprlnk.Visible = true;

                HttpContext.Current.Response.Redirect( HttpContext.Current.Request.Url.ToString() );
            }
            #endregion

            #region отмена переименов. названия темы
            if (e.CommandName == "СancelReName")
            {
                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null)
                    pnl1.Visible = false;

                HyperLink hprlnk = e.Item.FindControl("hprlnkTopic") as HyperLink;
                if (hprlnk != null)
                    hprlnk.Visible = true;

            }
            #endregion


        }

        protected void RptrTopic_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Админы и зарег.пользователи могут редактировать и удалять темы( польз. могут редакт. только свою тему)
            if (e.Item.DataItem != null)
            {
                if (Roles.IsUserInRole("admin"))
                {
                    LinkButton btn1 = e.Item.FindControl("lnkbtnEdit") as LinkButton;
                    if (btn1 != null)
                        btn1.Visible = true;
                    LinkButton btn2 = e.Item.FindControl("lnkbtnDelete") as LinkButton;
                    if (btn2 != null)
                        btn2.Visible = true;
                }
                else
                    if ( DataBinder.Eval(e.Item.DataItem, "topicAutor").ToString().Trim() == User.Identity.Name)
                    {
                        LinkButton btn1 = e.Item.FindControl("lnkbtnRename") as LinkButton;
                        if(btn1 != null) btn1.Visible = true;
                        LinkButton btn2 = e.Item.FindControl("lnkbtnDelete") as LinkButton;
                        if (btn2 != null) btn2.Visible = true;
                    }
            }


        }

    }
}