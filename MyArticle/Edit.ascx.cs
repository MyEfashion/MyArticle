using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.UI.WebControls;
using System.IO;
using System.Text;
using DotNetNuke.Entities.Content.Taxonomy;
using DevExpress.Web;

namespace MyArticle
{
    public partial class Edit : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Title_ASPxLabel.Text = LocalizeString("LabelTitle");
                Author_ASPxLabel.Text = LocalizeString("LabelAuthor");
                Tag_ASPxLabel.Text = LocalizeString("LabelTag");
                IsPublished_ASPxLabel.Text = LocalizeString("LabelIsPublished");
                IsComment_ASPxLabel.Text = LocalizeString("LabelIsComment");
                Thumbnail_ASPxLabel.Text = LocalizeString("LabelThumbnail");
                Description_ASPxLabel.Text = LocalizeString("LabelDescription");
                Thumbnail_ASPxLabel.Text = LocalizeString("LabelThumbnail");
                Body_ASPxLabel.Text = LocalizeString("LabelBody");



                InitBodyHtmlEditor();
                InitThumbnailPopupWindow();

                if(!string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
                {
                    MyArticleItem a = MyArticleManager.GetArticleByArtilceId( int.Parse(Request.QueryString["ArticleID"]));
                    Title_ASPxTextBox.Text = a.Title;
                    Description_ASPxTextBox.Text = a.Description;
                    Author_ASPxTextBox.Text = a.Author;
                    IsComment_ASPxCheckBox.Checked = a.IsComment;
                    IsPublished_ASPxCheckBox.Checked = a.IsPublished;
                    Body_ASPxHtmlEditor.Html = a.Body;
                    Thumbnail_ASPxImage.ImageUrl = a.ThumbnailUrl;
                }

                    IsPublished_ASPxCheckBox.Checked = true;
                    IsComment_ASPxCheckBox.Checked = true;
                    Thumbnail_ASPxImage.ImageUrl = "/DesktopModules\\MyArticle\\Image\\DefaultThumbnail.jpg";


                Terms_TermsSelector.Terms = ModuleContext.Configuration.Terms;
             

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

            if (!string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
            {
                a = MyArticleManager.GetArticleByArtilceId(int.Parse(Request.QueryString["ArticleId"]));
            }

            a.Title = Title_ASPxTextBox.Text.Trim();
            a.Description = Description_ASPxTextBox.Text.Trim();
            a.Body = Body_ASPxHtmlEditor.Html.Trim();
            a.IsPublished = IsPublished_ASPxCheckBox.Checked;
            a.IsComment = IsComment_ASPxCheckBox.Checked;
            a.PortalId = PortalSettings.PortalId;
            a.CreatedByUserId = PortalSettings.UserId;
            a.LastModifiedByUserId = PortalSettings.UserId;
            a.CreatedOnDate = DateTime.Now;
            a.LastModifiedOnDate = DateTime.Now;
            a.ArticleContentItem.Terms.AddRange(Terms_TermsSelector.Terms);
            
           
           

            if (Thumbnail_ASPxHiddenField.Contains("ImageUrl"))
            {
                a.ThumbnailUrl = Thumbnail_ASPxHiddenField["ImageUrl"].ToString();
            }
            else
            {
                a.ThumbnailUrl = Thumbnail_ASPxImage.ImageUrl;
            }
           
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
            else if(!string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
            {

                MyArticleManager.UpdateArticle(a, ModuleContext.TabId);
               
                callBack.JSProperties.Add("cpMsg", "Save Ok");
                e.Result = "Err";
            }
            else
            {

                MyArticleManager.AddArticle(a, ModuleContext.TabId);
                callBack.JSProperties.Add("cpMsg", "Save Ok");
                e.Result = "Err";
            }              

        }

        protected void InitBodyHtmlEditor()
        {
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.InitialFolder = MyArticleManager.GetArticleUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.UploadSettings.Enabled = true;
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.Enabled = true;
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.RootFolder = MyArticleManager.GetArticleUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.ThumbnailFolder = MyArticleManager.GetArticleThumbFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageSelector.EditingSettings.TemporaryFolder = MyArticleManager.GetArticleTempFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageUpload.UploadFolder = MyArticleManager.GetArticleUserFolder();
            Body_ASPxHtmlEditor.SettingsDialogs.InsertImageDialog.SettingsImageUpload.AdvancedUploadModeTemporaryFolder = MyArticleManager.GetArticleTempFolder();
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

            Thumbnail_ASPxFileManager.Settings.RootFolder = MyArticleManager.GetArticleUserFolder();
            Thumbnail_ASPxFileManager.Settings.ThumbnailFolder = MyArticleManager.GetArticleThumbFolder();

            //显示上传按钮
            Thumbnail_ASPxFileManager.SettingsContextMenu.Items.Add(new FileManagerToolbarUploadButton());
          
        
  
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



            string newImagePath = MyArticleManager.GetArticleThumbFolder()  + MyArticleManager.GetMD5HashFromFile(Server.MapPath(FileManager.SelectedFile.FullName)) + FileManager.SelectedFile.Extension;

           if(!System.IO.File.Exists(Server.MapPath(newImagePath)))
            {
                MyArticleManager.CompressImage(Server.MapPath(FileManager.SelectedFile.FullName), Server.MapPath(newImagePath), 100, 100);
            }

          

            FileManager.JSProperties.Add("cpResult", newImagePath);
       
            
        }


    }
}
