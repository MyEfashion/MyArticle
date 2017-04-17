<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="MyArticle.View" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<dx:ASPxDataView ID="ArticleList_ASPxDataView"
    runat="server"
    EnableTheming="True"
    Theme="Default">
    <SettingsTableLayout ColumnCount="1" />

    <PagerSettings ShowNumericButtons="False" Position="Bottom"></PagerSettings>
    <ItemStyle Border-BorderWidth="0px" Width="800px" Height="100px" />

</dx:ASPxDataView>


