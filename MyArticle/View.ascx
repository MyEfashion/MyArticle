<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="MyArticle.View" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<dx:ASPxDataView ID="ArticleList_ASPxDataView"
     runat="server"
     EnableTheming="True"
     Theme="Default" >
    <SettingsTableLayout ColumnCount="1" />
    <ItemTemplate>
        <div >
                <h6>
                    <asp:HyperLink runat="server" NavigateUrl='<%# EditUrl("ArticleId",DataBinder.Eval(Container.DataItem,"ArticleId").ToString(),"Detail")%>'>
                          <%# DataBinder.Eval(Container.DataItem,"Title")%>
                    </asp:HyperLink>
                </h6>
            </div>

        <div>
            <dx:ASPxImage Width="64px" Height="64px" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ThumbnailUrl") %>'></dx:ASPxImage>
            <label>
                  <%# DataBinder.Eval(Container.DataItem,"Description")%>
            </label>
        </div>

        <div>
           <label style="color:darkblue"> <%# DataBinder.Eval(Container.DataItem,"Author")%> </label> 
            发布于  <%# DataBinder.Eval(Container.DataItem,"CreatedOnDate")%>
            点击(<%# DataBinder.Eval(Container.DataItem,"ClickCount")%>)
        </div>
       
  
    </ItemTemplate>
    <PagerSettings ShowNumericButtons="False" Position="Bottom"></PagerSettings>
    <ItemStyle  Border-BorderWidth="0px"  Width="800px" Height="100px" />
    
</dx:ASPxDataView>



