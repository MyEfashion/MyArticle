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
using DotNetNuke.UI.Containers;

namespace MyArticle
{
    public partial class View : PortalModuleBase, IActionable
    {     
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
                List<MyArticleItem> articles = MyArticleManager.GetArticlesByTag(
                    int.Parse(ModuleConfiguration.ModuleSettings["PageSize"].ToString())*10,
                    0,
                    PortalId,
                    ResultSortType.ASC,
                    ModuleContext.Configuration.Terms[0].Name);
                ArticleList_ASPxDataView.SettingsTableLayout.RowsPerPage = int.Parse(ModuleConfiguration.ModuleSettings["PageSize"].ToString());
                Cache.Insert("ArticlesList", articles);                   
            }
            MyArticleDataBind();

        }

        ModuleActionCollection IActionable.ModuleActions
        {
            get
            {

                var actions = new ModuleActionCollection();


                actions.Add(GetNextActionID(), "Admin", "", "", "", EditUrl("Admin"), false, SecurityAccessLevel.Edit, true, false);


                actions.Add(GetNextActionID(), "Add Article",
                                       "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);

                return actions;
            }

        }

        protected void ArticleList_ASPxDataView_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            MyArticleDataBind();
        }

        private void MyArticleDataBind()
        {
            ArticleList_ASPxDataView.DataSource = (List<MyArticleItem>)Cache["ArticlesList"];
            ArticleList_ASPxDataView.DataBind();
        }

    }
}