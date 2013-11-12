<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReactivateAccount.aspx.cs" Inherits="MiniForum.Account.ReactivateAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    </span>

    <asp:Panel ID="pnlActivateAccount1" runat="server" Visible="false">
        <p>

            Если емейл 
            <asp:Label ID="lblEmail1" runat="server"  Font-Underline="true"  ForeColor="#284775" ></asp:Label>
            не поменялся, пройдите по
            <asp:LinkButton ID="lnkbtnActivateAccount1" runat="server" onclick="lnkbtnActivateAccount1_Click">этой ссылке,</asp:LinkButton>
            вам будет отправлено письмо с кодом активации.

         </p>
         <p>
            Если у вас другой емейл, укажите, пожалуйста, действующий e-mail, на который будет отправлено письмо, содержащее ссылку на страницу активации.
            <br />

            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxEmail" Display="Dynamic" 
                CssClass="failureNotification"  ValidationGroup="RegisterUserValidationGroup" ErrorMessage="Некорректный Email."
                EnableClientScript="true" ValidationExpression="^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"
                Font-Size="0.9em" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ControlToValidate="tbxEmail" Display="Dynamic" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" Font-Size="0.9em"
                ErrorMessage="RequiredFieldValidator" > Укажите E-mail</asp:RequiredFieldValidator>
                <br /> 
            <asp:TextBox ID="tbxEmail" runat="server" Width="251px" 
                 style="margin-bottom: 0px"></asp:TextBox>
             
            <asp:Button ID="btnActivateAccount1" runat="server"  
                ValidationGroup="RegisterUserValidationGroup" 
                style=" vertical-align:top; margin-left: 10px"  Text="получить письмо" 
                onclick="btnActivateAccount1_Click" />
        </p>
    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" Visible="false">
    <p>
        На Ваш почтовый ящик  отправлено письмо с кодом активации. Для завершения регистрации перейдите по ссылке из письма. 
        <br />Внимание!
        <br /> Процедуру активации необходимо произвести в период, который не превышает 1 день  с момента отправки письма. Аккаунты, не активированные в течение этого срока, удаляются автоматически из базы.
    </p>
    </asp:Panel>


</asp:Content>
