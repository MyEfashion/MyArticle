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
        private  int defaultPageSize = 10;
        private int defaultPageTotal = 10;
        private string defaultTermName = "";

        protected void Page_Load(object sender, EventArgs e)
        {         
            if(!IsPostBack)
            {


                if (ModuleConfiguration.ModuleSettings.Contains("PageSize") && !string.IsNullOrEmpty(ModuleConfiguration.ModuleSettings["PageSize"].ToString()))
                {
                    defaultPageSize = Convert.ToInt32(ModuleConfiguration.ModuleSettings["PageSize"].ToString());
                }

                if (ModuleConfiguration.ModuleSettings.Contains("DisplayStyle") && !string.IsNullOrEmpty(ModuleConfiguration.ModuleSettings["DisplayStyle"].ToString()))
                {
                    int style = Convert.ToInt32(ModuleConfiguration.ModuleSettings["DisplayStyle"].ToString());

                    if (style == 0)
                    {
                        ArticleList_ASPxDataView.ItemTemplate = Page.LoadTemplate("DesktopModules/MyArticle/Template/OnlyShowTitle.ascx");
                     }
                    else
                    {
                        ArticleList_ASPxDataView.ItemTemplate = Page.LoadTemplate("DesktopModules/MyArticle/Template/ShowTitleAndThumbneil.ascx");
                    }
                }

                if (ModuleContext.Configuration.Terms.Count > 0)
                {
                    defaultTermName = ModuleContext.Configuration.Terms[0].Name;
                }

                List<MyArticleItem> articles = MyArticleManager.GetArticlesByTag(defaultPageSize * defaultPageTotal, 0, PortalId, ResultSortType.ASC, defaultTermName);

                ArticleList_ASPxDataView.SettingsTableLayout.RowsPerPage = defaultPageSize;

                Cache.Insert("ArticlesList" + ModuleContext.ModuleId, articles);     

            }

            MyArticleDataBind();

        }


        /// <summary>
        /// 页面命令
        /// </summary>
        ModuleActionCollection IActionable.ModuleActions
        {
            get
            {

                var actions = new ModuleActionCollection();

                //跳转到文章管理面板
                actions.Add(GetNextActionID(), LocalizeString("CommandAdmin"), "", "", "", EditUrl("Admin"), false, SecurityAccessLevel.Edit, true, false);

                //跳转到添加文章面板
                actions.Add(GetNextActionID(), LocalizeString("CommandAddArticle"),
                                       "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);

                return actions;
            }

        }


        private void MyArticleDataBind()
        {
            ArticleList_ASPxDataView.DataSource = (List<MyArticleItem>)Cache["ArticlesList" + ModuleContext.ModuleId];
            ArticleList_ASPxDataView.DataBind();
            
        }

    }
}


