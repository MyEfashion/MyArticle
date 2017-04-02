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
        private int pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
                if(UserInfo.IsInRole("Administrators"))
                {
                    InitAdminSearchMenu();
                    pageSize = Article_ASPxGridView.SettingsPager.PageSize;
                    Session.Add("ArticleList", MyArticleManager.GetArticlesByPortalId(pageSize * 10, 0, PortalSettings.PortalId, ResultSortType.ASC));
                    MyArtilceDataBind();
                }
                else
                {
                    InitSearchMenu();
                    pageSize = Article_ASPxGridView.SettingsPager.PageSize;
                    Session.Add("ArticleList", MyArticleManager.GetArticlesByUserId(pageSize * 10, 0, PortalSettings.PortalId, ResultSortType.ASC, UserId));
                    MyArtilceDataBind();
                }
              
            }
     
        }

        protected void Search_ASPxButton_Click(object sender, EventArgs e)
        {
            string keywordType = Keyword_ASPxComboBox.SelectedItem.Value.ToString();
            string keyword = Keyword_ASPxTextBox.Text;

            if(keywordType == SearchArticleKeywordType.Title.ToString())
            {
                Session["ArticleList"] = MyArticleManager.GetArticlesByTitle(100, 0, PortalSettings.PortalId, ResultSortType.ASC, keyword);
             
            }
            else if(keywordType == SearchArticleKeywordType.Author.ToString())
            {
                Session["ArticleList"] = MyArticleManager.GetArticlesByAuthor(100, 0, PortalSettings.PortalId, ResultSortType.ASC, keyword);
            
            }
            else if(keywordType == SearchArticleKeywordType.User.ToString())
            {
                Session["ArticleList"] = MyArticleManager.GetArticlesByUserId(100, 0, PortalSettings.PortalId, ResultSortType.ASC, int.Parse(keyword));
                
            }


            MyArtilceDataBind();
        }

        protected void AddArticle_ASPxButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Edit"));
        }

        protected void InitAdminSearchMenu()
        {
            Keyword_ASPxComboBox.Items.Add( LocalizeString("Keyword_ASPxComboBox_Title"), SearchArticleKeywordType.Title);
            Keyword_ASPxComboBox.Items.Add( LocalizeString("Keyword_ASPxComboBox_Author"),SearchArticleKeywordType.Author);
            Keyword_ASPxComboBox.Items.Add(LocalizeString("Keyword_ASPxComboBox_User"), SearchArticleKeywordType.User);
            Keyword_ASPxComboBox.SelectedIndex = 0;

        }
        protected void InitSearchMenu()
        {
            Keyword_ASPxComboBox.Items.Add(LocalizeString("Keyword_ASPxComboBox_Title"), SearchArticleKeywordType.Title);
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
                MyArticleItem a = MyArticleManager.GetArticleByArtilceId( (int) grid.GetRowValues(grid.FocusedRowIndex, "ArticleId"));
                if(PortalSettings.UserInfo.IsSuperUser || a.CreatedByUserId == PortalSettings.UserId )
                {
                    MyArticleManager.DeleteArticle(int.Parse(grid.GetRowValues(grid.FocusedRowIndex, "ArticleId").ToString()));
                    grid.JSProperties.Add("cpDeleteResult", "OK");
                }
                else
                {
                    grid.JSProperties.Add("cpDeleteResult", "ERROR");
                }
                
            }
        }

 

        protected void Article_ASPxGridView_DataBinding(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxGridView).ForceDataRowType(typeof(List<>));
           

        }

        protected void Article_ASPxGridView_PageIndexChanged(object sender, EventArgs e)
        {
           MyArtilceDataBind();
        }

        private void MyArtilceDataBind()
        {
            if(Session["ArticleList"] != null)
            {
                Article_ASPxGridView.DataSource = (List<MyArticleItem>)Session["ArticleList"];
                Article_ASPxGridView.DataBind();
            }
            else
            {
                
                Session.Add("ArticleList", MyArticleManager.GetArticlesByPortalId(pageSize * 10, 0, PortalSettings.PortalId, ResultSortType.ASC));
            }
        }
    }
}