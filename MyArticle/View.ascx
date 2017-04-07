<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="MyArticle.View" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<dx:ASPxDataView ID="ArticleList_ASPxDataView" runat="server" EnableTheming="True" Theme="Default"  OnCustomCallback="ArticleList_ASPxDataView_CustomCallback" >
    <SettingsTableLayout ColumnCount="1" RowsPerPage="10" />
    <ItemTemplate>
        <div style="height: 80px; width: 120px; line-height: 80px; float: left; margin-right: 10px">
            <dx:ASPxImage runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ThumbnailUrl") %>'></dx:ASPxImage>

        </div>
        <div style="height: 80px; float: left">
            <div style="height: 40px">
                <h3>
                    <asp:HyperLink runat="server" NavigateUrl='<%# EditUrl("ArticleId",DataBinder.Eval(Container.DataItem,"ArticleId").ToString(),"Detail")%>'>
                          <%# DataBinder.Eval(Container.DataItem,"Title")%>

                    </asp:HyperLink>
                </h3>
            </div>
            <div style="height: 30px">
            </div>
        </div>
    </ItemTemplate>
    <PagerSettings ShowNumericButtons="False" Position="Bottom"></PagerSettings>
    <ItemStyle Paddings-Padding="0px" Border-BorderWidth="0px" Height="80px" Width="500px" />
    
</dx:ASPxDataView>



