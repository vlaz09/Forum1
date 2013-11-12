<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="RegisterDB.aspx.cs" Inherits="MiniForum.Account.Register" %>




<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent1" runat="server" ContentPlaceHolderID="MainContent">

        <span class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
                <br />
                <asp:Literal ID="ErrorMessage2" runat="server" ViewStateMode="Disabled"></asp:Literal>
            </span>

         
         <asp:Label ID="lblErrorSendMail" runat="server" Visible="false" Width="900px">Ваша учетная запись была создана. Для повторной отправки сообщения с кодом активации, пожалуйста, перейдите на
             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/LoginDB.aspx">страницу авторизации</asp:HyperLink>, 
             введите учетные данные и вам будет выслано сообщение с кодом авторизации. 
         </asp:Label>

     <asp:Panel ID="pnlRegistration" runat="server"  Visible="true" 
         style="margin-right: 0px" >
            <h2>
                Создать новую учетную запись
            </h2>
            <p>
                Используйте следующую форму для создания новой учетной записи.
            </p>
      
            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                    ValidationGroup="RegisterUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="register">
                    <legend>Сведения учетной записи</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Логин пользователя:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Display="Dynamic"
                                CssClass="failureNotification" ErrorMessage="Поле ''Имя пользователя'' является обязательным." ToolTip="Поле ''Имя пользователя'' является обязательным." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="UserName" Display="Dynamic" ErrorMessage="RegularExpressionValidator" 
                            ValidationExpression="[a-zA-Z0-9_-]{1,50}" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" Font-Size="0.9em">
                            Используйте только буквы английского алфавита и цифры. Макс. длина 50 знаков</asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">Электронная почта:</asp:Label>
                        <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email" Display="Dynamic" 
                            CssClass="failureNotification"  ValidationGroup="RegisterUserValidationGroup" ErrorMessage="Некорректный Email."
                            EnableClientScript="true" ValidationExpression="^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"
                            Font-Size="0.9em" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" Font-Size="0.9em">
                            Укажите E-mail</asp:RequiredFieldValidator>   
                                              
                    </p>
                        <p>
                        <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="tbxFirstName">Имя:</asp:Label>
                        <asp:TextBox ID="tbxFirstName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxFirstName" 
                                CssClass="failureNotification" ErrorMessage="Поле ''Имя'' является обязательным." ToolTip="Поле ''Имя'' является обязательным." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxFirstName" Display="Dynamic" 
                            ErrorMessage="RegularExpressionValidator"  ValidationExpression="[а-яА-Яa-zA-Z_-]{2,50}" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" Font-Size="0.9em">
                            Используйте только буквы русского (английского) алфавита</asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblLastName" runat="server" AssociatedControlID="tbxLastName" 
                            Width="851px">Фамилия:</asp:Label>
                        <asp:TextBox ID="tbxLastName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxLastName" 
                                CssClass="failureNotification" ErrorMessage="Поле ''Фамилия'' является обязательным." ToolTip="Поле ''Фамилия'' является обязательным." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxLastName" Display="Dynamic" 
                            ErrorMessage="RegularExpressionValidator"  ValidationExpression="[а-яА-Яa-zA-Z_-]{2,50}" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" Font-Size="0.9em">
                            Используйте только буквы русского (английского) алфавита</asp:RegularExpressionValidator>

                    </p>

                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Пароль:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                CssClass="failureNotification" ErrorMessage="Поле ''Пароль'' является обязательным." ToolTip="Поле ''Пароль'' является обязательным." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Подтвердите пароль:</asp:Label>
                        <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                ErrorMessage="Поле ''Подтвердите пароль'' является обязательным." ID="ConfirmPasswordRequired" runat="server" 
                                ToolTip="Поле ''Подтвердите пароль'' является обязательным." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="Значения ''Пароль'' и ''Подтвердите пароль'' должны совпадать."
                                ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                    </p>
                </fieldset>
                <p >
                    <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Создать пользователя" 
                            ValidationGroup="RegisterUserValidationGroup"  onclick="CreateUserButton_Click"  />
                 </p>
            </div>
     </asp:Panel>
    <div>
        <asp:Label ID="lblSuccessfulreg" runat="server" Width="900px" Visible="false">Регистрация завершена! 
            <br /> Письмо с инструкцией было выслано на указанный Вами e-mail. Пожалуйста, прочитайте письмо и выполните необходимые действия, чтобы завершить регистрацию.
        </asp:Label>
    </div>
</asp:Content>
