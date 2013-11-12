using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using MiniForum;
using System.Text;


namespace MiniForum.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           // RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {

            Page.Validate();
            if (!Page.IsValid) return;
            UserDAL userdal = new UserDAL();

            try
            {   
                //Проверяет на уникальность логина и емейла
                bool[] isLogMail = userdal.IsUserLoginEmail(UserName.Text.Trim(), Email.Text.Trim());
                if (isLogMail[0] || isLogMail[1])
                {
                    if (isLogMail[0]) ErrorMessage.Text = "Пользователь с таким логином уже существует";
                    if (isLogMail[1]) ErrorMessage2.Text = "Пользователь с таким E-mail уже существует";
                    return;
                }

                else
                {
                    string login = UserName.Text.Trim();
                    string email = Email.Text.Trim();
                    string userFirstLastName = tbxFirstName.Text + " " + tbxLastName.Text;

                    Guid pass = AppCode.GetHashEncoding(ConfirmPassword.Text.Trim());//хеш пароля сохраняется в БД

                    Guid userGuid = AppCode.GetHashEncoding(pass.ToString());//Хеш хеша пароля служит кодом активации аккаунта пользователя

                    string htmlTextMess = WriteEmailMessage(userFirstLastName, login, userGuid, Request.Url.Authority);
                    
                    Email mail = new Email();
                    bool flagSaveDB = false;
                    try
                    {
                        userdal.AddNewUser(login, pass, userFirstLastName, email);

                        flagSaveDB = true;
                        mail.SendEmail(email, "MiniForum - Вы были зарегистрированы", htmlTextMess);

                        pnlRegistration.Visible = false;
                        lblSuccessfulreg.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = ex.Message;
                        if (flagSaveDB)
                        {
                            pnlRegistration.Visible = false;
                            lblErrorSendMail.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            { ErrorMessage.Text = ex.Message; }

        }

        //Генерация сообщения пользователю для активации уч.записи
        internal string WriteEmailMessage(string userName, string userLogin, Guid userGuid, string dnsSiteMiniForum)//dnsSiteMiniForum - Нужен для определения домена сайта
        {
            StringBuilder htmlTextMess = new StringBuilder();
            htmlTextMess.Append("<p>Здравствуйте ");
            htmlTextMess.Append(userName);
            htmlTextMess.Append("! <br />  Вы были зарегистрированы на сайте MiniForum.");
            htmlTextMess.Append(" Для подтверждения регистрации требуется перейти по следующей ссылке(если к вам это письмо попало случайно то просто удалите это письмо ):</p>");

            string url = String.Format("{0}/Account/ConfirmRegistration.aspx?login={1}&guid={2}", dnsSiteMiniForum, userLogin, userGuid);
            string href = String.Format("<a href=\"{0}\"> {0} </a>", url);
            htmlTextMess.Append(href);


            htmlTextMess.Append("<br /> <p> Сразу после подтверждение можете войти на сайт");
            htmlTextMess.Append("<br /> Имя пользователя (логин):  ");
            htmlTextMess.Append(userLogin);
            htmlTextMess.Append("</p><p>C Уважением, Администратор сайта.");
            htmlTextMess.Append("<br /><br />Внимание: Это письмо было автоматически отправлено с сайта MiniForum.</p>");
            return htmlTextMess.ToString();
        
        }




        
    }
}
