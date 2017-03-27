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
    ///<summary>
    /// ArticleController provides the implementation of methods for our article
    ///</summary>
    public static class MyArticleManager
    {
           
        ///<summary>
        /// Get an  article by article Id
        ///</summary>
        ///<param name="articleId"></param>
        ///<returns></returns>
        public static MyArticleItem GetArticle(int articleId)
        {

            return CBO.FillObject<MyArticleItem>(SqlDataProvider.Instance().GetArticleById(articleId));
        }

        ///<summary>
        /// Get a list of articles by term for a portal
        ///</summary>
        ///<param name="portalId"></param>
        ///<returns></returns>      
        public static List<MyArticleItem> GetArticlesByTag(int pageSize, int pageIndex, int portalId, int sortAsc, string tagName, bool published)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByTag(pageSize, pageIndex, portalId, sortAsc, tagName, published));
        }

        /// <summary>
        /// Get a list of articles  for a portal
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="portalId"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        public static List<MyArticleItem> GetArticles(int pageSize, int pageIndex, int portalId, int sortAsc)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticles(pageSize, pageIndex, portalId, sortAsc));
        }

        /// <summary>
        /// Search article by title
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="portalId"></param>
        /// <param name="sortAsc"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static List<MyArticleItem> SearchArticlesByTitle(int pageSize, int pageIndex, int portalId, int sortAsc, string title)
        {   //todo: look at caching
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().SearchArticlesByTitle(pageSize, pageIndex, portalId, sortAsc, title));
        }


        public static List<MyArticleItem> SearchArticlesByAuthor(int pageSize, int pageIndex, int portalId, int sortAsc, string author)
        {   //todo: look at caching
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().SearchArticlesByTitle(pageSize, pageIndex, portalId, sortAsc, author));
        }

        public static List<MyArticleItem> GetArticlesByUser(int pageSize, int pageIndex, int portalId, int sortAsc, int userId)
        {
            return CBO.FillCollection<MyArticleItem>(SqlDataProvider.Instance().GetArticlesByUser(pageSize, pageIndex, portalId, sortAsc, userId));

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
            var a = GetArticle(articleId);

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

        public static string GetArticleImageRootFolder()
        {
            return PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/";
        }

        public static string GetArticleImageUserFolder()
        {
            return PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/" + PortalSettings.Current.UserId + "/";
        }

        public static string GetArticleImageThumbFolder()
        {

            return PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/Thumb/";
        }

        public static string GetArticleImageTempFolder()
        {

            return PortalSettings.Current.HomeDirectory + MyArticleGlobal.ArticleImageFolderName + "/Temp/";
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



        /// <summary>
        /// 
        /// </summary>
        //public static IFolderInfo GetArticleFolder()
        //{

        //    if (FolderManager.Instance.FolderExists(PortalSettings.Current.PortalId, GetArticleImageFolder()))
        //    {
        //        return FolderManager.Instance.GetFolder(PortalSettings.Current.PortalId, GetArticleImageFolder());
        //    }
        //    else
        //    {
        //        IFolderInfo folder = FolderManager.Instance.AddFolder(PortalSettings.Current.PortalId, GetArticleImageFolder());

        //        ArrayList permissionArr = PermissionController.GetPermissionsByFolder();

        //        foreach (PermissionInfo permission in permissionArr)
        //        {
        //            FolderManager.Instance.SetFolderPermission(folder,
        //                permission.PermissionID, RoleController.
        //                Instance.GetRoleByName(PortalSettings.Current.PortalId, "Administrators").RoleID,
        //                PortalSettings.Current.UserId);
        //        }

        //        return folder;
        //    }

        //}
    }
}
