<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="RegisterDB.aspx.cs" Inherits="MiniForum.Account.RegisterDB" %>




<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent1" runat="server" ContentPlaceHolderID="MainContent">

        <span class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
            </span>

         
         <asp:Label ID="lblErrorSendMail" runat="server" Visible="false" Width="900px">Ваша учетная запись была создана. Для повторной отправки сообщения с кодом активации, пожалуйста, перейдите на
             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/LoginDB.aspx">страницу авторизации</asp:HyperLink>, 
             введите учетные данные и вам будет выслано сообщение с кодом авторизации. 
         </asp:Label>

     <asp:Panel ID="pnlRegistration" runat="server"  Visible="true" 
         style="margin-right: 0px" >
         <table border="0" width="400px" style="height: 45px" class="accountInfo">


             <tr>
                 <td colspan="2" align="center" style="color: Silver;">
                     Используйте следующую форму для создания новой учетной записи.
                 </td>
             </tr>

             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Логин пользователя:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtbxUserLogin"
                         Display="Dynamic" CssClass="failureNotification" ErrorMessage="Поле ''Имя пользователя'' является обязательным."
                         ToolTip="Поле ''Имя пользователя'' является обязательным." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtbxUserLogin"
                         Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="[a-zA-Z0-9_-]{1,50}"
                         ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification"
                         Font-Size="0.9em">Используйте только буквы английского алфавита и цифры. Макс. длина 50 знаков</asp:RegularExpressionValidator>
                     <asp:TextBox ID="txtbxUserLogin" runat="server" CssClass="textEntry" Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Электронная почта:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbxEmail"
                         Display="Dynamic" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup"
                         ErrorMessage="Некорректный Email." EnableClientScript="true" ValidationExpression="^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"
                         Font-Size="0.9em" />
                     <asp:TextBox ID="txtbxEmail" runat="server" CssClass="textEntry" Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Имя:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxFirstName"
                         CssClass="failureNotification" ErrorMessage="Поле ''Имя'' является обязательным."
                         ToolTip="Поле ''Имя'' является обязательным." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxFirstName"
                         Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="[а-яА-Яa-zA-Z_-]{1,50}"
                         ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification"
                         Font-Size="0.9em">
                            Используйте только буквы русского (английского) алфавита. Макс.длина 50 знаков</asp:RegularExpressionValidator>
                        <asp:TextBox ID="tbxFirstName" runat="server" CssClass="textEntry" Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Фамилия:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxLastName"
                         CssClass="failureNotification" ErrorMessage="Поле ''Имя'' является обязательным."
                         ToolTip="Поле ''Имя'' является обязательным." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxLastName"
                         Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="[а-яА-Яa-zA-Z_-]{1,50}"
                         ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification"
                         Font-Size="0.9em">
                            Используйте только буквы русского (английского) алфавита. Макс.длина 50 знаков</asp:RegularExpressionValidator>
                     <asp:TextBox ID="tbxLastName" runat="server" CssClass="textEntry" Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Пароль:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtbxPassword"
                         CssClass="failureNotification" ErrorMessage="Поле ''Пароль'' является обязательным."
                         ToolTip="Поле ''Пароль'' является обязательным." ValidationGroup="RegisterUserValidationGroup"
                         Font-Size="0.9em">Поле ''Пароль'' является обязательным.</asp:RequiredFieldValidator>
                     <asp:TextBox ID="txtbxPassword" runat="server" CssClass="passwordEntry" TextMode="Password"
                         Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td align="right" width="130px" style="padding: 5px">
                     Подтвердите пароль:
                 </td>
                 <td align="right" style="padding: 5px">
                     <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtbxPassword"
                         ControlToValidate="txtbxConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                         ErrorMessage="Значения ''Пароль'' и ''Подтвердите пароль'' должны совпадать."
                         ValidationGroup="RegisterUserValidationGroup" Font-Size="0.9em">Значения ''Пароль'' и ''Подтвердите пароль'' должны совпадать.</asp:CompareValidator>
                     <asp:TextBox ID="txtbxConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"
                         Width="220px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td colspan="2" align="right" >
                     <asp:Button ID="btnCreateUser" runat="server" CommandName="MoveNext" Text="Создать пользователя"
                         ValidationGroup="RegisterUserValidationGroup" OnClick="BtnCreateUser_Click" />
                 </td>
             </tr>
         </table>
     </asp:Panel>
    <div>
        <asp:Label ID="lblSuccessfulreg" runat="server" Width="900px" Visible="false">Регистрация завершена! 
            <br /> Письмо с инструкцией было выслано на указанный Вами e-mail. Пожалуйста, прочитайте письмо и выполните необходимые действия, чтобы завершить регистрацию.
        </asp:Label>
    </div>
</asp:Content>
