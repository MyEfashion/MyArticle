<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="MyArticle.View" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


 

<dx:ASPxDataView ID="ArticleList_ASPxDataView" runat="server" EnableTheming="True" Theme="Youthful" OnPageIndexChanged="ArticleList_ASPxDataView_PageIndexChanged" OnPageIndexChanging="ArticleList_ASPxDataView_PageIndexChanging" OnCustomCallback="ArticleList_ASPxDataView_CustomCallback" OnDataBinding="ArticleList_ASPxDataView_DataBinding">
    <SettingsTableLayout ColumnCount="1" />
    <ItemTemplate>
         <dx:ASPxImage  runat="server"  ImageUrl=<%# DataBinder.Eval(Container.DataItem,"ThumbnailUrl") %> ></dx:ASPxImage>
         <dx:ASPxLabel  runat="server" Text=<%# DataBinder.Eval(Container.DataItem,"Title") %> ></dx:ASPxLabel>
    </ItemTemplate>
<PagerSettings  ShowNumericButtons="False" Position="Bottom"></PagerSettings>
</dx:ASPxDataView>
