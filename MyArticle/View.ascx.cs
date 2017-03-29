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
        //override protected void OnInit(EventArgs e)

        //{

        //    InitializeComponent();

        //    base.OnInit(e);

        //}



        //private void InitializeComponent()

        //{

        //    Load += Page_Load;

        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            InitSearchMenu();
            //int pageSize = Article_ASPxGridView.SettingsPager.PageSize;
            //Article_ASPxGridView.DataSource = MyArticleManager.GetArticles(pageSize*10, 0, 0, 1);
            //Article_ASPxGridView.DataBind();          
        }

        protected void Search_ASPxButton_Click(object sender, EventArgs e)
        {
            string keywordType = Keyword_ASPxComboBox.SelectedItem.Value.ToString();
            string keyword = Keyword_ASPxTextBox.Text;

            if(keywordType == SearchArticleKeywordType.Title.ToString())
            {
                Article_ASPxGridView.DataSource = MyArticleManager.SearchArticlesByTitle(100, 0, 0, 0, keyword);
            }
            else if(keywordType == SearchArticleKeywordType.Author.ToString())
            {
                Article_ASPxGridView.DataSource = MyArticleManager.SearchArticlesByAuthor(100, 0, 0, 0, keyword);
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
            Keyword_ASPxComboBox.SelectedIndex = 0;

        }

        protected void Article_ASPxGridView_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }

        protected void Article_ASPxGridView_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }
    }
}