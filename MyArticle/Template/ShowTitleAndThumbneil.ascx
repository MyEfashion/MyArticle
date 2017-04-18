<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowTitleAndThumbneil.ascx.cs" Inherits="MyArticle.ShowTitleAndThumbneil" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<div style="float: left; margin-right: 10px;">
    <dx:aspximage width="64px" height="64px" runat="server" imageurl='<%# Eval("ThumbnailUrl") %>'></dx:aspximage>
</div>



<div style="float: left">

    <div>
        <h6>
            <asp:HyperLink runat="server" NavigateUrl='<%# EditUrl("ArticleId",Eval("ArticleId").ToString(),"Detail")%>'>
                          <%# Eval("Title")%>
            </asp:HyperLink>
        </h6>
    </div>



    <div>
        <label style="color: blue"><%# Eval("Author")%> </label>

        <label><%= LocalizeString("PublishedDateLabel.Text")%> </label>

        <%# Eval("CreatedOnDate")%>

        <label><%= LocalizeString("ClickCountLabel.Text")%> </label>

        (<%# Eval("ClickCount")%>)

    </div>


</div>
