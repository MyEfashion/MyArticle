<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlyShowTitle.ascx.cs" Inherits="MyArticle.OnlyShowTitle" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


        <div style="float: left">
            
            <div>
                <h6>
                    <asp:HyperLink runat="server" NavigateUrl='<%# EditUrl("ArticleId", Eval("ArticleId").ToString(),"Detail")%>'>
                          <%# Eval("Title")%>
                    </asp:HyperLink>
                </h6>
            </div>



            <div>
                <label style="color:blue"><%# Eval("Author")%> </label>

                <label><%= LocalizeString("PublishedDateLabel.Text")%> </label>

                <%# Eval("CreatedOnDate")%>

                <label><%= LocalizeString("ClickCountLabel.Text")%> </label>

                (<%# Eval("ClickCount")%>)

            </div>


        </div>
