using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using System.IO;
using System.Text;

namespace MyArticle
{
    public partial class Edit : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitBodyHtmlEditor();
                InitThumbnailPopupWindow();

                if(!string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
                {
                    MyArticleItem a = MyArticleManager.GetArticleByArtilceId( int.Parse(Request.QueryString["ArticleID"]));
                    Title_ASPxTextBox.Text = a.Title;
                    Description_ASPxMemo.Text = a.Description;
                    Author_ASPxTextBox.Text = a.Author;
                    IsComment_ASPxCheckBox.Checked = a.IsComment;
                    IsPublished_ASPxCheckBox.Checked = a.IsPublished;
                    Body_ASPxHtmlEditor.Html = a.Body;
                    Thumbnail_ASPxImage.ImageUrl = a.ThumbnailUrl;
                }
                else
                {
                    IsPublished_ASPxCheckBox.Checked = true;
                    IsComment_ASPxCheckBox.Checked = true;
                    Thumbnail_ASPxImage.ImageUrl = "/DesktopModules\\MyArticle\\Image\\DefaultThumbnail.jpg";
                }

            }

        }


        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveArticle_ASPxCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            MyArticleItem a = new MyArticleItem();

            a.Title = Title_ASPxTextBox.Text.Trim();
            a.Description = Description_ASPxMemo.Text.Trim();
            a.Body = Body_ASPxHtmlEditor.Html.Trim();
            a.IsPublished = IsPublished_ASPxCheckBox.Checked;
            a.IsComment = IsComment_ASPxCheckBox.Checked;
            a.PortalId = PortalSettings.PortalId;
            a.CreatedByUserId = PortalSettings.UserId;
            a.LastModifiedByUserId = PortalSettings.UserId;
            a.CreatedOnDate = DateTime.Now;
            a.LastModifiedOnDate = DateTime.Now;
            a.ThumbnailUrl = Thumbnail_ASPxHiddenField["ImageUrl"].ToString();
            a.Author = Author_ASPxTextBox.Text.Trim();

            DevExpress.Web.ASPxCallback callBack = (DevExpress.Web.ASPxCallback)source;

            if (a.Title.Length <= 0 || a.Title.Length > MyArticleGlobal.ArticleTitleMaxLength)
            {
              
                callBack.JSProperties.Add("cpMsg", "Title Error");
                e.Result = "Err";
                return;
            }
            else if(a.Description.Length <= 0 || a.Description.Length > MyArticleGlobal.ArticleDescriptionMaxLength)
            {
                callBack.JSProperties.Add("cpMsg", "Description Error");
                e.Result = "Err";
                return;
            }
            else if(a.Body.Length <=0 )
            {
                callBack.JSProperties.Add("cpMsg", "Body Error");
                e.Result = "Err";
                return;
            }
            else if(a.Author.Length <=0 || a.Author.Length > MyArticleGlobal.ArticleAuthorMaxLength)
            {
                callBack.JSProperties.Add("cpMsg", "Author Error");
                e.Result = "Err";
                return;
            }
            else
            {
                MyArticleManager.AddArticle(a, ModuleContext.TabId);
            }              

        }

        protected void InitBodyHtmlEditor()
        {
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.InitialFolder = MyArticleManager.GetArticleImageUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.UploadSettings.Enabled = true;
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.Enabled = true;
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.RootFolder = MyArticleManager.GetArticleImageUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.ThumbnailFolder = MyArticleManager.GetArticleImageThumbFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.EditingSettings.TemporaryFolder = MyArticleManager.GetArticleImageTempFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageUpload.UploadFolder = MyArticleManager.GetArticleImageUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageUpload.AdvancedUploadModeTemporaryFolder = MyArticleManager.GetArticleImageTempFolder();
        }

        /// <summary>
        /// 初始化缩略图弹出窗口
        /// </summary>
        protected void InitThumbnailPopupWindow()
        {
            Thumbnail_ASPxPopupControl.PopupHorizontalAlign = DevExpress.Web.PopupHorizontalAlign.WindowCenter;
            Thumbnail_ASPxPopupControl.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.WindowCenter;
            Thumbnail_ASPxPopupControl.CloseAction = DevExpress.Web.CloseAction.CloseButton;
            Thumbnail_ASPxPopupControl.AllowDragging = true;
            Thumbnail_ASPxPopupControl.Modal = true;
            Thumbnail_ASPxPopupControl.Windows[0].HeaderText = "Select Thumbnail";
            Thumbnail_ASPxPopupControl.Width = 800;
            Thumbnail_ASPxPopupControl.Height = 400;

            Thumbnail_ASPxFileManager.Settings.RootFolder = MyArticleManager.GetArticleImageUserFolder();
            Thumbnail_ASPxFileManager.Settings.ThumbnailFolder = MyArticleManager.GetArticleImageThumbFolder();
        
  
        }

        /// <summary>
        /// 取消编辑文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel_ASPxButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl());
        }

        /// <summary>
        /// 选择缩略图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Thumbnail_ASPxFileManager_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            DevExpress.Web.ASPxFileManager FileManager = (DevExpress.Web.ASPxFileManager)sender;



            string newImagePath = MyArticleManager.GetArticleImageThumbFolder()  + MyArticleManager.GetMD5HashFromFile(Server.MapPath(FileManager.SelectedFile.FullName)) + FileManager.SelectedFile.Extension;

           if(!System.IO.File.Exists(Server.MapPath(newImagePath)))
            {
                MyArticleManager.CompressImage(Server.MapPath(FileManager.SelectedFile.FullName), Server.MapPath(newImagePath), 100, 100);
            }

          

            FileManager.JSProperties.Add("cpResult", newImagePath);
       
            
        }


    }
}
