<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MiniForum._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="~/Styles/Forum.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForumTopic.aspx" Text="К списку тем >>"></asp:HyperLink> <br /></h2>

    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" /> <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    








</asp:Content>

