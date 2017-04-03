using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyArticle
{
    public partial class Settings :   ModuleSettingsBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Settings.Contains("PageSize"))
                {
                    PageSize_ASPxTextBox.Text = Settings["PageSize"].ToString();

                }             

                if (Settings.Contains("DisplayStyle"))
                {
                    DisplayStyle_ASPxComboBox.SelectedIndex = Int32.Parse( Settings["DisplayStyle"].ToString());
                }

                DisplayStyle_ASPxComboBox.DataSource = MyArticle.ArticleListDisplayStyle.Styles;
                DisplayStyle_ASPxComboBox.DataBind();

            }


        }

        public override void UpdateSettings()
        {
            ModuleController.Instance.UpdateModuleSetting(ModuleId, "PageSize", PageSize_ASPxTextBox.Text);
            ModuleController.Instance.UpdateModuleSetting(ModuleId, "DisplayStyle", DisplayStyle_ASPxComboBox.SelectedIndex.ToString());
        }
    }

   
}