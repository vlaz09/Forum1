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
    public partial class RegisterDB : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           // RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void BtnCreateUser_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            UserDAL userdal = new UserDAL();

            try
            {   
                //Проверяет на уникальность логин и емейл
                bool isLogMail = userdal.IsUserLoginEmail(txtbxUserLogin.Text.Trim(), txtbxEmail.Text.Trim());
                if (isLogMail)
                {
                    ErrorMessage.Text = "Пользователь с таким логином или E-mail уже существует";
                    return;
                }
                else
                {
                    string login = txtbxUserLogin.Text.Trim();
                    string email = txtbxEmail.Text.Trim();
                    string userFirstLastName = tbxFirstName.Text + " " + tbxLastName.Text;

                    string pass = AppCode.GetHashEncoding(txtbxConfirmPassword.Text.Trim());//хеш пароля сохраняется в БД
                    string identityCode = AppCode.GetHashEncoding(pass.ToString());//Хеш хеша пароля служит кодом активации аккаунта пользователя
                    string htmlTextMess = WriteEmailMessage(userFirstLastName, login, identityCode, Request.Url.Authority);
                    
                    bool flagSaveDB = false;
                    try
                    {
                        userdal.AddNewUser(login, pass, userFirstLastName, email);
                        flagSaveDB = true;
                        Mail.SendEmail(email, "MiniForum - Вы были зарегистрированы", htmlTextMess);

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
        internal string WriteEmailMessage(string userName, string userLogin, string identityCode, string dnsSiteMiniForum)//dnsSiteMiniForum - Нужен для определения домена сайта
        {
            StringBuilder htmlTextMess = new StringBuilder();
            htmlTextMess.Append("<p>Здравствуйте ");
            htmlTextMess.Append(userName);
            htmlTextMess.Append("! <br />  Вы были зарегистрированы на сайте MiniForum.");
            htmlTextMess.Append(" Для подтверждения регистрации требуется перейти по следующей ссылке(если к вам это письмо попало случайно то просто удалите это письмо ):</p>");

            string url = String.Format("{0}/Account/ConfirmRegistration.aspx?login={1}&guid={2}", dnsSiteMiniForum, userLogin, identityCode);
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
