using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;

namespace MyArticle
{
    public partial class View : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitSearchMenu();
            if(!IsPostBack)
            {
                int pageSize = Article_ASPxGridView.SettingsPager.PageSize;
                Article_ASPxGridView.DataSource = MyArticleManager.GetArticlesByPortalId(pageSize * 10, 0, 0,  ResultSortType.ASC);
                Article_ASPxGridView.DataBind();
            }
     
        }

        protected void Search_ASPxButton_Click(object sender, EventArgs e)
        {
            string keywordType = Keyword_ASPxComboBox.SelectedItem.Value.ToString();
            string keyword = Keyword_ASPxTextBox.Text;

            if(keywordType == SearchArticleKeywordType.Title.ToString())
            {
                Article_ASPxGridView.DataSource = MyArticleManager.GetArticlesByTitle(100, 0, 0, 0, keyword);
            }
            else if(keywordType == SearchArticleKeywordType.Author.ToString())
            {
                Article_ASPxGridView.DataSource = MyArticleManager.GetArticlesByAuthor(100, 0, 0, 0, keyword);
            }
            else if(keywordType == SearchArticleKeywordType.User.ToString())
            {
                Article_ASPxGridView.DataSource = MyArticleManager.GetArticlesByAuthor(100, 0, 0, 0, keyword);
            }

           
            Article_ASPxGridView.DataBind();
        }

        protected void AddArticle_ASPxButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Edit"));
        }

        protected void InitSearchMenu()
        {
            Keyword_ASPxComboBox.Items.Add( LocalizeString("Keyword_ASPxComboBox_Title"), SearchArticleKeywordType.Title);
            Keyword_ASPxComboBox.Items.Add( LocalizeString("Keyword_ASPxComboBox_Author"),SearchArticleKeywordType.Author);
            Keyword_ASPxComboBox.Items.Add(LocalizeString("Keyword_ASPxComboBox_User"), SearchArticleKeywordType.User);
            Keyword_ASPxComboBox.SelectedIndex = 0;

        }

        protected void Article_ASPxGridView_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            DevExpress.Web.ASPxGridView grid = (DevExpress.Web.ASPxGridView)sender;
            if(e.ButtonID == "Edit_ASPxGridViewCommand")
            {
                grid.JSProperties.Add("cpUrl", EditUrl("ArticleId", grid.GetRowValues(grid.FocusedRowIndex, "ArticleId").ToString(), "Edit"));                   
            }
            else if(e.ButtonID == "View_ASPxGridViewCommand")
            {
                grid.JSProperties.Add("cpUrl", EditUrl("ArticleId", grid.GetRowValues(grid.FocusedRowIndex, "ArticleId").ToString(), "Edit"));
            }
            else if(e.ButtonID== "Delete_ASPxGridViewCommand")
            {

            }
        }

        protected void Article_ASPxGridView_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void Article_ASPxGridView_DataBinding(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxGridView).ForceDataRowType(typeof(List<>));

        }
    }
}