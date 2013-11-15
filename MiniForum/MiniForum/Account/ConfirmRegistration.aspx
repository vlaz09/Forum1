<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmRegistration.aspx.cs" Inherits="MiniForum.Account.ConfirmRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    </span>

    <div>
        <asp:Label ID="Label1" runat="server"  Visible="false"> Ваша учетная запись активирована. Вы можете войти на сайт!</asp:Label>
    </div>
    
    <div>
        <asp:Label ID="Label2" runat="server" Visible="false"> Код активации неправильный. Пользователя с таким логином нет. 
            Для регистрации перейдите на 
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/RegisterDB.aspx">страницу регистрации.</asp:HyperLink> 
        </asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Visible="false">Для повторного получения письма с кодом активации перейдите 
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/LoginDB.aspx">по ссылке.</asp:HyperLink> 
         </asp:Label>
     </div>
</asp:Content>