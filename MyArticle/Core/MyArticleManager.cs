using System;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using MyArticle;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Security.Permissions;
using System.Collections;
using DotNetNuke.Security.Roles;
using System.IO;
using System.Drawing.Imaging;
using System.Text;

namespace MyArticle
{

    public static class MyArticleManager
    {
           
        public static MyArticleItem GetArticleByArtilceId(int articleId)
        {

            return CBO.FillObject<MyArticleItem>(SqlDataProvider.Instance().GetArticleByArticleId(articleId));
        }    
        public static List<MyArticleItem> GetArticlesByTag(int pageSize, int pageIndex, int portalId, ResultSortType sort, string tag)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByTag(pageSize, pageIndex, portalId, sort, tag));
        }
        public static List<MyArticleItem> GetArticlesByPortalId(int pageSize, int pageIndex, int portalId, ResultSortType sort)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByPortalId(pageSize, pageIndex, portalId, sort));
        }
        public static List<MyArticleItem> GetArticlesByTitle(int pageSize, int pageIndex, int portalId, ResultSortType sort, string title)
        {   
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByTitle(pageSize, pageIndex, portalId, sort, title));
        }
        public static List<MyArticleItem> GetArticlesByAuthor(int pageSize, int pageIndex, int portalId, ResultSortType sort, string author)
        {   
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByAuthor(pageSize, pageIndex, portalId, sort, author));
        }
        public static List<MyArticleItem> GetArticlesByUserId(int pageSize, int pageIndex, int portalId, ResultSortType sort, int userId)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByUserId(pageSize, pageIndex, portalId, sort, userId));

        }

        ///<summary>
        /// Add a article
        ///</summary>
        ///<param name="a"></param>
        ///<param name="tabId"></param>
        ///<returns></returns>
        public static int AddArticle(MyArticleItem a, int tabId)
        {
            var content = new MyArticleContent();

            var contentItem = content.CreateContentItem(a, tabId);

            a.ContentItemId = contentItem.ContentItemId;

            a.ArticleId = SqlDataProvider.Instance().AddArticle(a);

            UpdateArticle(a, tabId);

            return a.ArticleId;
        }

        /// <summary>
        /// Update a exiting article
        /// </summary>
        /// <param name="a"></param>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public static int UpdateArticle(MyArticleItem a, int tabId)
        {

            SqlDataProvider.Instance().UpdateArticle(a);

            var content = new MyArticleContent();

            content.UpdateContentItem(a, tabId);

            return a.ArticleId;



        }


        ///<summary>
        /// Delete an article based on ID
        ///</summary>
        ///<param name="articleId"></param>
        public static int DeleteArticle(int articleId)
        {

            //get the article
            var a = GetArticleByArtilceId(articleId);

            var cntTaxonomy = new MyArticleContent();
            //delete the content item
            cntTaxonomy.DeleteContentItem(a);
            //delete the article
            SqlDataProvider.Instance().DeleteArticle(articleId);
            return 1;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static string GetArticleLink(int tabId, int articleId)
        {

            return DotNetNuke.Common.Globals.NavigateURL(tabId, "", "aid=" + articleId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        public static void AddClickCount(int articleId)
        {
            SqlDataProvider.Instance().AddClickCount(articleId);
        }

        /// <summary>
        /// Get count of article 
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public static int GetArticlesCount(int portalId)
        {
            return SqlDataProvider.Instance().GetArticlesCount(portalId);
        }


        public static bool HasEditPermission(MyArticleItem a)
        {
            if (PortalSettings.Current.UserInfo.IsSuperUser || PortalSettings.Current.UserId == a.CreatedByUserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
  
        /// <summary>
        /// 压缩图片，并返回压缩后的图片名称
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static void CompressImage(string oldImagePath,string newImagePath, int maxWidth, int maxHeight)
        {
         
            try
            {

                System.Drawing.Bitmap newImg;
                System.Drawing.Image oldImg = System.Drawing.Image.FromFile(oldImagePath);

                int imgWidthTo = oldImg.Width;
                int imgHeightTo = oldImg.Height;


                if (oldImg.Width > oldImg.Height)  //如果宽度超过高度以宽度为准来压缩
                {
                    if (oldImg.Width > maxWidth) //如果图片宽度超过限制,则压缩图片
                    {
                        imgWidthTo = maxWidth;
                        imgHeightTo = imgHeightTo / (oldImg.Width / maxHeight);
                    }

                    newImg = new System.Drawing.Bitmap(oldImg, imgWidthTo, imgHeightTo);
                }
                else
                {
                    if (oldImg.Height > maxHeight)
                    {
                        imgHeightTo = maxHeight;
                        imgWidthTo = imgWidthTo / (oldImg.Height / maxWidth);
                    }

                    newImg = new System.Drawing.Bitmap(oldImg, imgWidthTo, imgHeightTo);
                }
                newImg.Save(newImagePath);
            }
            catch (Exception e)
            {
                return ;
            }
        }

        public static string GetArticleRootFolder()
        {
            string path = PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/";
            if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/"))
            {
                return path;
            }
            else
            {
                FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/");
                return path;
            }
           
        }

        public static string GetArticleUserFolder()
        {
            string path = PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/" + PortalSettings.Current.UserId + "/";
            if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/" + PortalSettings.Current.UserId + "/"))
            {
                return path;
            }
            else
            {
                FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/" + PortalSettings.Current.UserId + "/");
                return path;
            }
        }

        public static string GetArticleThumbFolder()
        {

            string path = PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/Thumb/";
            if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/Thumb/"))
            {
                return path;
            }
            else
            {
                FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/Thumb/");
                return path;
            }
        }

        public static string GetArticleTempFolder()
        {

            string path = PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/Temp/";
            if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/Temp/"))
            {
                return path;
            }
            else
            {
                FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, MyArticleGlobal.ArticleImageFolderName + "/Temp/");
                return path;
            }
        }

        public static string GetMD5HashFromFile(String fileName)
        {
            String hashMD5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.MD5 calculator = System.Security.Cryptography.MD5.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashMD5 = stringBuilder.ToString();
                }//关闭文件流
            }//结束计算
            return hashMD5;

        }



 
        public static IFolderInfo GetArticleFolder()
        {

            if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, "" ))
            {
                return FolderManager.Instance.GetFolder(PortalSettings.Current.PortalId, "");
            }
            else
            {
                IFolderInfo folder = FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, "");

                ArrayList permissionArr = PermissionController.GetPermissionsByFolder();

                foreach (PermissionInfo permission in permissionArr)
                {
                    FolderManager.Instance.SetFolderPermission(folder,
                        permission.PermissionID, RoleController.
                        Instance.GetRoleByName(PortalSettings.Current.PortalId, "Administrators").RoleID,
                        PortalSettings.Current.UserId);
                }

                return folder;
            }

        }
    }
}
