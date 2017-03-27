
using System;
using DotNetNuke.Entities.Modules;


namespace MyArticle
{

    public class MyArticleModuleBase : PortalModuleBase
    {
      
        public int ArticleId
        {
            get
            {
                var qs = Request.QueryString["aid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }
        }


        public int PageIndex
        {
            get
            {
                var qs = Request.QueryString["PageIndex"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return 0;
            }
        }

        public int PageSize
        {
            get
            {
                if(Settings.Contains("PageSize"))
                    return Convert.ToInt32(Settings["PageSize"]);
                return 10;
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId,"PageSize",value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public string GetArticleLink(string articleId)
        {
            return MyArticleManager.GetArticleLink(TabId, Convert.ToInt32(articleId));
        }
    }

}
