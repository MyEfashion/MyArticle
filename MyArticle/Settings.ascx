<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="MyArticle.Settings" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

  
<table style="border-collapse: separate; border-spacing: 10px;">
    <tr>
        <td><dx:ASPxLabel runat="server" Text="PageSize" Theme="Youthful"></dx:ASPxLabel></td>
        <td>   <dx:ASPxTextBox runat="server" ID="PageSize_ASPxTextBox" Theme="Youthful"></dx:ASPxTextBox></td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel runat="server" Text="Tag" Theme="Youthful"></dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxComboBox runat="server" ID="DisplayStyle_ASPxComboBox" Theme="Youthful"></dx:ASPxComboBox>
        </td>
    </tr>
</table>
           
     
  


