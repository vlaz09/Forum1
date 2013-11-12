<%@ Page Title="Выполнить вход" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="LoginDB.aspx.cs" Inherits="MiniForum.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    
    <h2>
        Выполнить вход
    </h2>
    <p>
        Введите имя пользователя и пароль.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Регистрация</asp:HyperLink> если у вас нет учетной записи.
    </p>

      <span class="failureNotification">
          <asp:Literal ID="ErrorMassage" runat="server"></asp:Literal>
       </span>

    <asp:Panel ID="pnlRegistration" runat="server" Visible="true" Height="497px">
        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
            <LayoutTemplate>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                     ValidationGroup="LoginUserValidationGroup"/>
                <div class="accountInfo">
                    <fieldset class="login">
                        <legend>Сведения учетной записи</legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Имя пользователя:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                 CssClass="failureNotification" ErrorMessage="Поле ''Имя пользователя'' является обязательным." ToolTip="Поле ''Имя пользователя'' является обязательным." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Пароль:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                 CssClass="failureNotification" ErrorMessage="Поле ''Пароль'' является обязательным." ToolTip="Поле ''Пароль'' является обязательным." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>

                    </fieldset>
                    
                        <asp:Button ID="LoginButton" runat="server" onclick="LoginButton_Click" 
                            style="margin-bottom: 3px" Text="Выполнить вход" 
                            ValidationGroup="LoginUserValidationGroup" />
                    
                </div>
            </LayoutTemplate>
        </asp:Login>
    </asp:Panel>

    <asp:Panel ID="pnlReactivate" runat="server" Visible="false" Height="129px" 
        style="margin-top: 0px" Width="738px">
        <p style="width: 721px">
            <asp:Label ID="Label1" runat="server" Text="Label"> Ваша учетная запись не была активирована. Если Вы не получили письмо, 
                содержащее ссылку на страницу активации, просим Вас запросить письмо еще раз, используя
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">эту ссылку.</asp:LinkButton>
            </asp:Label>
         </p>
    </asp:Panel>


</asp:Content>
