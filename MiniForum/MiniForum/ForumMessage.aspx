<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForumMessage.aspx.cs" Inherits="MiniForum.ForumMessage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="~/Styles/Forum.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #TextArea1
        {
            width: 571px;
            height: 174px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <h2><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForumTopic.aspx" Text="К списку тем >>"></asp:HyperLink> <br /></h2>

   
                      
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    </span>

    <div class="header">
        <table   border="1" width="100%" >
            <tr style="background:#5388B4;background: #4b6c9e;"> 
                <td width=80% style="border: 0px; color: #f9f9f9">             
                    <h2 style="padding: 0px 0px 0px 20px; "> 
                    
                    Тема: <asp:Label ID="lblTopic" runat="server" Text="тема сообщений форума"></asp:Label>
                    </h2>
                </td>  
                <td width=20% style="border:0px; vertical-align:top; ">                
                    <h3 style="color: #f9f9f9; vertical-align:top; "> Сообщение за 
                                <asp:DropDownList id="DrplstPeriod" AutoPostBack="True" runat="server" 
                                    onselectedindexchanged="DrplstPeriod_SelectedIndexChanged" 
                                    style="margin-left: 0px"  >
                                    <asp:ListItem Selected="True" Value="All"> Все </asp:ListItem>
                                    <asp:ListItem Value="LastMonth"> Последний месяц </asp:ListItem>
                                    <asp:ListItem Value="LastWeek"> Последнюю неделю </asp:ListItem>
                                    <asp:ListItem Value="LastMess"> Последнее сообщение </asp:ListItem>
                                </asp:DropDownList>
                    </h3> 
                </td> 
             </tr >
        </table>
	</div>

     <asp:ScriptManager ID="ScriptManager1" runat="server" />


        <asp:Repeater id="RptrMessage" runat="server" 
            onitemcommand="RptrMessage_ItemCommand" 
            onitemdatabound="RptrMessage_ItemDataBound" >
              <HeaderTemplate>
              </HeaderTemplate>
              <ItemTemplate>

              <table   border="1" width="100%" style="height: 45px" >
                    <tr style="background:#5388B4;color:#FFF"> 
                        <td width=80% style="border: 0px;  padding: 5px"> <%# DataBinder.Eval(Container.DataItem, "UserLogin")%></td>  
                        <td style="border:0px; padding: 5px " width=20% > <%# DataBinder.Eval(Container.DataItem, "AddDate")%></td> </tr>
                    <tr >
                        <td  width=100% colspan="2" style="margin-left: 20%; padding: 10px">
                    
                            <asp:Label ID="lblTextMess" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Text")%>' ></asp:Label>

                            <asp:Panel ID="pnlEdit" runat="server" Width="906px" Visible="false" Height="107px">
                                    <asp:TextBox ID="txtbxEditMess" runat="server" Height="70px" Rows="3" TextMode="MultiLine" Width="894px" Text='<%#Eval("Text")%>'></asp:TextBox>
                                    <div style=" vertical-align:top; margin-left: 10px">
                                        <asp:Button ID="btnEditMess" runat="server" Text="Сохранить" Height="26px" Width="125px" CommandName="EditMess" CommandArgument='<%# Eval("MessagesID") %>' />&nbsp&nbsp
                                        <asp:Button ID="btnСancelReName" runat="server" Height="26px" Text="Отменить" Width="85px" CommandName="СancelEditMess"/>
                                    </div>
                            </asp:Panel>
                            <br />
                            <br />
                            <div height="1" style="border-top: 1px dotted #333333; ">
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Редактировать сообщение" CommandName="Edit" CommandArgument='<%# Eval("MessagesID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Удалить сообщение" CommandName="Delete" CommandArgument='<%# Eval("MessagesID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                            </div>
                        </td>
                    </tr>
              </table>

              </ItemTemplate>
              <FooterTemplate>
              </FooterTemplate>
        </asp:Repeater>


    <asp:Panel  ID="pnlAddNewMess" runat="server">
        <div >
         <h2>Добавить сообщение</h2>
            <h3>Cообщение: <asp:TextBox ID="txtbxAddMess" runat="server" Rows="20" TextMode="MultiLine" 
                Height="119px" Width="596px" style=" vertical-align:top; margin-left: 10px"></asp:TextBox>
                <br /></h3> 
              <p style=" vertical-align:top; margin-left: 50px"> <asp:Button ID="BtnAddMess" runat="server" onclick="BtnAddMess_Click" Text="Добавить" /></p>
        </div>
    </asp:Panel>    
    
</asp:Content>

