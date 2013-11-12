using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MiniForum
{
    public partial class ForumMessage : System.Web.UI.Page
    {
        int topid;
        string mess;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificationRequestParams();
            if (!IsPostBack)
            {
                string mess = Request.Params["mess"];
                if (mess == "LastMess")
                    Session["Period"] = "LastMess";
                if (topid != 0)
                    dataFillingRptr1(topid);
                else
                {
                    lblTopic.Text = "Тема не найдена";
                    pnlAddNewMess.Visible = false;
                }
            }
        }



        private void VerificationRequestParams()
        {
            if (Page.RouteData.Values["topicid"] != null)//{categoryid}/ForumTopic/{topicid}/Message
            {
                try
                {
                    topid = Convert.ToInt32(Page.RouteData.Values["topicid"]);
                }
                catch (Exception ex)
                {
                    string text = " Message: " + ex.Message +
                            " StackTrace: " + ex.StackTrace;
                    ErrorDAL.AddNewError(DateTime.Now, text, "");

                    pnlAddNewMess.Visible = false;
                    ErrorMessage.Text = "У вас нет разрешения на доступ к этой странице!";
                }
            }
            else
                if (Request.Params["topicid"] != null)// url вида: ForumMessage.aspx?topicid = 
                {
                    try
                    {
                        topid = Convert.ToInt32(Request.Params["topicid"]);
                    }
                    catch (Exception ex)
                    {
                        string text = " Message: " + ex.Message +
                                " StackTrace: " + ex.StackTrace;
                        ErrorDAL.AddNewError(DateTime.Now, text, "");

                        pnlAddNewMess.Visible = false;
                        ErrorMessage.Text = "У вас нет разрешения на доступ к этой странице!";
                    }
                }
        }

        //Период отображения сообщений
        protected void DrplstPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = DrplstPeriod.SelectedValue.ToString();
            Session["Period"] = s;

            if (topid != 0)
            dataFillingRptr1(topid);
        }


        //Выбор из сессии периода отображ. сообщений, 
        //Получ. данных из БД и привязка их к Repeater
        private void dataFillingRptr1(int topID)
        {
            MessagesDAL fmess = new MessagesDAL();
            List<Messages> listMessages = new List<Messages>();

            try
            {
                if ((Session["Period"] == null) || (Session["Period"] == "All"))
                {
                    listMessages = fmess.GetMessagesByTopic(topID);
                }

                if ((Session["Period"] != null) && (Session["Period"] == "LastMonth"))
                {
                    DateTime dt = new DateTime();
                    dt = DateTime.Now.AddDays(-31);
                    listMessages = fmess.GetMessagesByTopic(topID, dt);

                    DrplstPeriod.SelectedIndex = 1;
                }
                if ((Session["Period"] != null) && (Session["Period"] == "LastWeek"))
                {
                    DateTime dt = new DateTime();
                    dt = DateTime.Now.AddDays(-7);
                    listMessages = fmess.GetMessagesByTopic(topID, dt);

                    DrplstPeriod.SelectedIndex = 2;
                }

                if ((Session["Period"] != null) && (Session["Period"] == "LastMess"))
                {
                    listMessages = fmess.GetLastMessage(topID);
                    DrplstPeriod.SelectedIndex = 3;
                }

                //Название темы в заголовок «Тема:» 
                //привязки данных к  Repeater
                if (listMessages.Count != 0)
                {
                    lblTopic.Text = " " + listMessages[0].TopicName;//Назв. темы берется из первого сообщ.

                    RptrMessage.DataSource = listMessages; //привязки данных к  Repeater
                    RptrMessage.DataBind();
                }

                else //сообщений нет, тогда Назв. темы берется из БД (по ИД из урл-строки параметр topid)
                {
                    TopicDAL ftop = new TopicDAL();
                    string topName = ftop.GetTopicName(topid);
                    if (topName != "")
                        lblTopic.Text = topName;
                    else
                        lblTopic.Text = "Тема не найдена";
                }
            }
            catch (Exception ex)
            { 
                ErrorMessage.Text = ex.Message;
                pnlAddNewMess.Visible = false;
            }
        
        }
        
        protected void RptrMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
          #region вкл.отображ. панели для редакт. сообщ. 
            if (e.CommandName == "Edit") //LinkButton CommandName="Edit"
            {
                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null)
                    pnl1.Visible = true;

                Label lbl = e.Item.FindControl("lblTextMess") as Label;
                if (lbl != null)
                    lbl.Visible = false;
            }
         #endregion

         #region Удалить сообщ.
            if (e.CommandName == "Delete") //LinkButton CommandName="Delete"
            {
                MessagesDAL fmess = new MessagesDAL();
                if (e.CommandArgument != null)
                    fmess.DeleteMessage(Convert.ToInt32(e.CommandArgument));
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
          #endregion

          #region редакт. сообщ
            if (e.CommandName == "EditMess") //Button CommandName=EditMess
            {

                TextBox txtbx1 = e.Item.FindControl("txtbxEditMess") as TextBox;
                if (txtbx1 != null)
                {
                    MessagesDAL fmess = new MessagesDAL();

                    if (e.CommandArgument != null)
                        fmess.UpdateMessage(Convert.ToInt32(e.CommandArgument), txtbx1.Text.ToString());
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                }

                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null)
                    pnl1.Visible = false;

                Label lbl = e.Item.FindControl("lblTextMess") as Label;
                if (lbl != null)
                    lbl.Visible = true;
            }
          #endregion

          #region отмена редакт. сообщ
            if (e.CommandName == "СancelEditMess") //Button CommandName=СancelEditMess
            {
                Panel pnl1 = e.Item.FindControl("pnlEdit") as Panel;
                if (pnl1 != null)
                    pnl1.Visible = false;

                Label lbl = e.Item.FindControl("lblTextMess") as Label;
                if (lbl != null)
                    lbl.Visible = true;
            }
          #endregion

        }

        protected void RptrMessage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Проверка роли для редакт. сообщений
            if (e.Item.DataItem != null)
            {
                //string[] s = Roles.GetRolesForUser();
                if (Roles.IsUserInRole("admin"))
                {
                    LinkButton btn1 = e.Item.FindControl("lnkbtnEdit") as LinkButton;
                    btn1.Visible = true;
                    LinkButton btn2 = e.Item.FindControl("lnkbtnDelete") as LinkButton;
                    btn2.Visible = true;
                }
                else
                    if (DataBinder.Eval(e.Item.DataItem, "autorName").ToString().Trim() == User.Identity.Name)
                    {
                        LinkButton btn1 = e.Item.FindControl("lnkbtnEdit") as LinkButton;
                        if (btn1 != null) btn1.Visible = true;
                        LinkButton btn2 = e.Item.FindControl("lnkbtnDelete") as LinkButton;
                        if (btn2 != null) btn2.Visible = true;
                    }
            }
        }

        //добавить нов.сообщ.
        protected void btnAddMess_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                string text = txtbxAddMess.Text.ToString();
                MessagesDAL fmess = new MessagesDAL();
                try
                {
                    fmess.AddNewMessage(topid, User.Identity.Name, text);
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
                }
                catch (Exception ex)
                { ErrorMessage.Text =  ex.Message; }
            }

            else FormsAuthentication.RedirectToLoginPage();
        }
    }
}



//protected void Timer1_Tick(object sender, EventArgs e)
        //{
            
        //        UpdatePanel1.Update();

        //        Label l1 = UpdatePanel1.FindControl("Label1") as Label;
        //        if (l1 != null)
        //            l1.Text = DateTime.Now.ToLongTimeString();
            
        //}