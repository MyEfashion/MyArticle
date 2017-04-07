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
    public partial class Detail : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
            {
                MyArticleItem a = MyArticleManager.GetArticleByArtilceId(int.Parse(Request.QueryString["ArticleId"]));
                Title_Literal.Text = a.Title;
                Body_Literal.Text = a.Body;
               
            }
        }
    }
}