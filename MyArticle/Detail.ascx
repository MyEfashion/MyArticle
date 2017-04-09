
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detail.ascx.cs" Inherits="MyArticle.Detail" %>

<style type="text/css">
    .Detail_Title{
        text-align:center;
    }
    .Detail_Body{

    }
</style>



<div class="Detail_Title">
    <h3>
    <asp:Literal ID="Title_Literal" runat="server"></asp:Literal>
    </h3>
</div>



<div class="Detail_Body">
    <asp:Literal ID="Body_Literal" runat="server"></asp:Literal>
</div>

<br />
<br />
<br />

<div id="uyan_frame" >

</div >
