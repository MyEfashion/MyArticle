<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="MyArticle.Admin" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<table style="border-collapse: separate; width: 100%; border-spacing: 10px;">
    <tr>
        <td colspan="2">
            <div style="display: inline-block; vertical-align: bottom">
                <dx:ASPxComboBox Height="30px" ID="Keyword_ASPxComboBox" runat="server" ValueType="System.String" Theme="Office2003Olive">
                </dx:ASPxComboBox>
            </div>
            <div style="display: inline-block; vertical-align: bottom">
                <dx:ASPxTextBox Height="30px" ID="Keyword_ASPxTextBox" runat="server" Width="170px" Theme="Office2003Olive"></dx:ASPxTextBox>
            </div>
            <div style="display: inline-block; vertical-align: bottom">
                <dx:ASPxButton Height="30px" ID="Search_ASPxButton" runat="server" Text="Search" Theme="Office2003Olive" OnClick="Search_ASPxButton_Click"></dx:ASPxButton>
            </div>
        </td>
        <td>
            <dx:ASPxButton ID="AddArticle_ASPxButton" runat="server" Text="Add Article" OnClick="AddArticle_ASPxButton_Click" Theme="Office2003Olive"></dx:ASPxButton>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <dx:ASPxGridView OnDataBinding="Article_ASPxGridView_DataBinding" KeyFieldName="ArticleId" ID="Article_ASPxGridView"  ClientInstanceName="Article_ASPxGridView" OnCustomButtonCallback="Article_ASPxGridView_CustomButtonCallback" runat="server" EnableTheming="True" Theme="Office2003Olive" AutoGenerateColumns="False" Width="623px" OnPageIndexChanged="Article_ASPxGridView_PageIndexChanged">
                <ClientSideEvents CustomButtonClick="function(s, e) {
                        e.processOnServer = true;

}"
                    EndCallback="function(s,e)
                    {
                    if(s.cpUrl)
                    {
                     window.open(s.cpUrl,'_blank ');
                    }
                    if(s.cpDeleteResult == 'OK')
                    {
                     alert('OK');
                    }
                    if(s.cpDeleteResult == 'ERROR')
                    {
                     alert('Error');
                    }
                   

                    }" />

                <SettingsCommandButton>
                    <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

                    <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
                </SettingsCommandButton>
                <SettingsBehavior AllowFocusedRow="true" />

                <Columns>
                    <dx:GridViewDataColumn Caption="ArticleId" Name="ArticleId" FieldName="ArticleId">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="Title" Caption="Title" FieldName="Title">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="CreatedOnDate" Caption="CreatedOnDate" FieldName="CreatedOnDate">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="LastModifiedOnDate" Caption="LastModifiedOnDate" FieldName="LastModifiedOnDate">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="ClickCount" Caption="ClickCount" FieldName="ClickCount">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="CreatedByUserId" Caption="CreatedByUserId" FieldName="CreatedByUserId">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="LastModifiedByUserId" Caption="LastModifiedByUserId" FieldName="LastModifiedByUserId">
                    </dx:GridViewDataColumn>
                     <dx:GridViewDataColumn Name="Author" Caption="Author" FieldName="Author">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataCheckColumn Caption="IsPublished" FieldName="IsPublished" >
                    </dx:GridViewDataCheckColumn>
                     <dx:GridViewDataCheckColumn Caption="IsComment" FieldName="IsComment" >
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewCommandColumn Caption="Setting" >
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Delete_ASPxGridViewCommand" Text="Delete"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Edit_ASPxGridViewCommand" Text="Edit"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="View_ASPxGridViewCommand" Text="View"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>

                </Columns>

            </dx:ASPxGridView>
        </td>
    </tr>
</table>








