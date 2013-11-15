<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForumTopic.aspx.cs" Inherits="MiniForum.ForumTopic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                   
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" ViewStateMode="Disabled"></asp:Literal>
    </span>

    <asp:Repeater id="RptrTopic" runat="server" onitemcommand="RptrTopic_ItemCommand" 
        onitemdatabound="RptrTopic_ItemDataBound" >
          <HeaderTemplate>
          <table   border="1" width="100%" cellpadding="1"   >
          </HeaderTemplate>
             
          <ItemTemplate>

                <tr   style="  background:#F7F6F3; color:#333333"> 
                    <td  width=60% style="border: 0px; text-align:left; vertical-align: middle; padding:  1px 10px 3px 10px"> 
                        <h2 style="color:#333333">
                        <asp:HyperLink ID="hprlnkTopic" runat="server" Text='<%#Eval("TopicName")%>' NavigateUrl='<%#String.Format("~/ForumMessage.aspx?topicid={0}", Eval("TopicID"))%>' Visible="true" ForeColor="#333333"  Font-Overline="false"  /> 
                        <asp:Panel ID="pnlEdit" runat="server" Width="611px" Visible="false" Height="81px">
                            <asp:TextBox ID="txtbxTopicName" runat="server" Height="40px" Rows="3" TextMode="MultiLine" Width="584px" Text='<%#Eval("TopicName")%>'></asp:TextBox>
                            <div style=" vertical-align:top; margin-left: 10px">
                            <asp:Button ID="btnSaveReName" runat="server" Text="Сохранить" Height="26px" Width="85px" CommandName="SaveReName" CommandArgument='<%# Eval("TopicID") %>' />&nbsp&nbsp
                            <asp:Button ID="btnСancelReName" runat="server" Height="26px" Text="Отменить" Width="85px" CommandName="СancelReName"/>
                            </div>
                        </asp:Panel>
                        </h2>
                        Тема создана <%# DataBinder.Eval(Container.DataItem, "AddDate")%>,  
                        автор <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserLogin")%>' NavigateUrl='<%#String.Format("~/Account/UserProfile.aspx?userid={0}", Eval("UserID"))%>' ForeColor="#3366CC" Font-Overline="false"/>
                        <br />
                        <br />
                        <div height="1" style="border-top: 1px dotted #333333; ">
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Переименовать тему" CommandName="Rename" CommandArgument='<%# Eval("TopicID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Удалить тему" CommandName="Delete" CommandArgument='<%# Eval("TopicID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                        </div>

                    </td>  

                     <td  width=10% style="border: 0px; text-align:center "> 
                        <h2 > <%# DataBinder.Eval(Container.DataItem, "countReply")%></h2>
                        сообщений
                    </td>
                    <td width=20% style="border: 0px;  "> 
                        <asp:HyperLink ID="hprlnkLastMess" runat="server" Text='Последнее' NavigateUrl='<%#String.Format("~/ForumMessage.aspx?topicid={0}&mess={1}", Eval("TopicID"), "LastMess" )%>' ForeColor="#3366CC" Font-Overline="false"/>
                        от  <asp:HyperLink ID="hprlnkLastMessAutor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserLoginLastMess")%>' NavigateUrl='<%#String.Format("~/Account/UserProfile.aspx?userid={0}", Eval("UserIDLastMess"))%>' ForeColor="#3366CC" Font-Overline="false"/>
                        <br /><%# DataBinder.Eval(Container.DataItem, "LastMessData")%>
                    </td>
                </tr>

          </ItemTemplate>
          <AlternatingItemTemplate>

          <tr   style="  background:white; color:#284775"> 
                    <td  width=60% style="border: 0px; text-align:left; vertical-align: middle; padding:  1px 10px 3px 10px"> 
                        <h2 style="color:#284775">
                            <asp:HyperLink ID="hprlnkTopic" runat="server" Text='<%#Eval("TopicName")%>' NavigateUrl='<%#String.Format("~/ForumMessage.aspx?topicid={0}", Eval("TopicID"))%>' Visible="true" ForeColor="#284775"  Font-Overline="false"  /> 
                            
                            <asp:Panel ID="pnlEdit" runat="server" Width="611px" Visible="false" Height="81px">
                                <asp:TextBox ID="txtbxTopicName" runat="server" Height="40px" Rows="3" TextMode="MultiLine" Width="584px" Text='<%#Eval("TopicName")%>'></asp:TextBox>
                                <div style=" vertical-align:top; margin-left: 10px">
                                <asp:Button ID="btnSaveReName" runat="server" Text="Сохранить" Height="26px" Width="125px" CommandName="SaveReName" CommandArgument='<%# Eval("TopicID") %>' />&nbsp&nbsp
                                <asp:Button ID="btnСancelReName" runat="server" Height="26px" Text="Отменить" Width="85px" CommandName="СancelReName"/>
                                </div>
                            </asp:Panel>
                        </h2>

                        Тема создана <%# DataBinder.Eval(Container.DataItem, "AddDate")%>,  
                        автор <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserLogin")%>' NavigateUrl='<%#String.Format("~/Account/UserProfile.aspx?userid={0}", Eval("UserID"))%>' ForeColor="#3366CC" Font-Overline="false"/>
                    
                        <br />
                        <br />
                        <div height="1" style="border-top: 1px dotted #333333; ">
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Переименовать тему" CommandName="Rename" CommandArgument='<%# Eval("TopicID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Удалить тему" CommandName="Delete" CommandArgument='<%# Eval("TopicID") %>' Visible="False"></asp:LinkButton>&nbsp&nbsp
                        </div>
                    </td>  

                     <td  width=10% style="border: 0px; text-align:center "> 
                        <h2 > <%# DataBinder.Eval(Container.DataItem, "countReply")%></h2>
                        сообщений
                    </td>
                    <td width=20% style="border: 0px;  "> 
                        <asp:HyperLink ID="hprlnkLastMess" runat="server" Text='Последнее' NavigateUrl='<%#String.Format("~/ForumMessage.aspx?topicid={0}&mess={1}", Eval("TopicID"), "LastMess" )%>' ForeColor="#3366CC" Font-Overline="false"/>
                        от  <asp:HyperLink ID="hprlnkLastMessAutor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserLoginLastMess")%>' NavigateUrl='<%#String.Format("~/Account/UserProfile.aspx?userid={0}", Eval("UserIDLastMess"))%>' ForeColor="#3366CC" Font-Overline="false"/>
                        <br /><%# DataBinder.Eval(Container.DataItem, "LastMessData")%>
                    </td>
                </tr>

          </AlternatingItemTemplate>
             
          <FooterTemplate>
          </table>  
          </FooterTemplate>
             
       </asp:Repeater>

    <asp:Panel ID="pnlAddNewTopic" runat="server">
         <div >
         <h2>Новая тема</h2>
            <h3>Название 
                <asp:TextBox ID="TextBox1" runat="server" Rows="5" TextMode="MultiLine" 
                Height="33px" Width="596px" style=" vertical-align:top; margin-left: 10px"></asp:TextBox>
                <br /></h3> 
              <p style=" vertical-align:top; margin-left: 50px"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Создать" /></p>
       </div>
    </asp:Panel>

</asp:Content>
