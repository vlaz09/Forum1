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
        private int categoryid = 1;//раздел темы,(  1 заглушка пока разделы не созданы)
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificationRequestParams();

            if (!IsPostBack)
            {
                DataFillingRptr(categoryid);
            }
        }

        //Получает параметр categoryid из строки адреса
        private void VerificationRequestParams()
        {
            int number;
            if (Page.RouteData.Values["categoryid"] != null) //если url вида: {categoryid}/ForumTopic
            {
                if (Int32.TryParse(Page.RouteData.Values["categoryid"].ToString(), out number))
                    categoryid = number;
            }
            else
                if (Request.Params["categoryid"] != null)//если url вида: ForumTopic.aspx?categoryid = 
                {
                    if (Int32.TryParse(Request.Params["categoryid"].ToString(), out number))
                        categoryid = number;
                }
        }

        //привязка репитора к данным 
        private void DataFillingRptr(int ID)
        {
            TopicDAL topicdal = new TopicDAL();
            try
            {
                List<Entities.Topic> listTopic = topicdal.GetTopics(categoryid);
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
            if ( SecurityManager.GetCurrentUser(Request) )
            {
                string text = TextBox1.Text;
                TopicDAL topicdal = new TopicDAL();
                try
                {
                    int topicid = topicdal.AddNewTopic(SessionManager.UserID, text, categoryid);
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
                TopicDAL topicdal = new TopicDAL();
                
                if (e.CommandArgument != null)
                try
                {
                    topicdal.DeleteTopic(Convert.ToInt32(e.CommandArgument));
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
                        TopicDAL topicdal = new TopicDAL();
                        try
                        {
                            string topName = txtbx1.Text.ToString();

                            if (e.CommandArgument != null)
                                topicdal.UpdateTopic(Convert.ToInt32(e.CommandArgument), topName);
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
                if (SessionManager.UserRole == "admin")
                {
                    LinkButton btn1 = e.Item.FindControl("lnkbtnEdit") as LinkButton;
                    if (btn1 != null)
                        btn1.Visible = true;
                    LinkButton btn2 = e.Item.FindControl("lnkbtnDelete") as LinkButton;
                    if (btn2 != null)
                        btn2.Visible = true;
                }
                else
                    if (DataBinder.Eval(e.Item.DataItem, "UserID").ToString().Trim() == SessionManager.UserID.ToString())
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