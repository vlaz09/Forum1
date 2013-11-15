<%@ Page Title="Выполнить вход" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="LoginDB.aspx.cs" Inherits="MiniForum.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style3
        {
            width: 93%;
        }
        .style5
        {
            width: 93%;
            height: 65px;
        }
        .style6
        {
            width: 94%;
            height: 65px;
        }
        .style7
        {
            width: 94%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
      <span class="failureNotification">
          <asp:Literal ID="ErrorMassage" runat="server" ViewStateMode="Disabled"></asp:Literal>
       </span>
    <asp:Panel ID="pnlRegistration" runat="server" Visible="true" Height="497px" 
        Width="782px">
        <h2>
            Выполнить вход
        </h2>
        <div>
            Введите имя пользователя и пароль.
            <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Регистрация</asp:HyperLink>
            если у вас нет учетной записи.
        </div>
        <br />
        <table border="0" width="400px" style="height: 45px" class="accountInfo" >
            <tr>
                <td colspan="2" align="center" style="color: Silver;">
                    Сведения учетной записи
                </td>
            </tr>

            <tr >
                <td align="right" width="130px"  style="padding: 5px">
                    Имя пользователя:</td>
                <td align="right" style="padding: 5px" >
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtbxLoginUser"
                        CssClass="failureNotification" ErrorMessage="Поле ''Имя пользователя'' является обязательным."
                        ToolTip="Поле ''Имя пользователя'' является обязательным." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtbxLoginUser" runat="server" CssClass="textEntry" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="130px" style="padding: 5px">
                    Пароль:
                </td>
                <td align="right" style="padding: 5px">
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtbxPassword"
                        CssClass="failureNotification" ErrorMessage="Поле ''Пароль'' является обязательным."
                        ToolTip="Поле ''Пароль'' является обязательным." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtbxPassword" runat="server" CssClass="passwordEntry" TextMode="Password" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right" style="color: Silver;">
                    <asp:Button ID="bntLogin" runat="server" OnClick="BntLogin_Click" Style="margin-bottom: 3px"
                        Text="Выполнить вход" ValidationGroup="LoginUserValidationGroup" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlReactivate" runat="server" Visible="false" Height="129px" 
        style="margin-top: 0px" Width="738px">
        <div style="width: 721px">
            <asp:Label ID="Label1" runat="server" Text="Label"> Ваша учетная запись не была активирована. Если Вы не получили письмо, 
                содержащее ссылку на страницу активации, просим Вас запросить письмо еще раз, используя
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">эту ссылку.</asp:LinkButton>
            </asp:Label>
         </div>
    </asp:Panel>


</asp:Content>
