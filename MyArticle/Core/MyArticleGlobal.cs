using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyArticle
{
    public class MyArticleGlobal
    {
        public static int ArticleTitleMaxLength = 64;
        public static int ArticleAuthorMaxLength = 32;
        public static int ArticleDescriptionMaxLength = 128;
        public static string ArticleImageFolderName = "MyArticle";
        public static string UploadFileFilter = "bmp,gif,jpeg,jpg,jpe,png";
        public static string ArticleThumbnailDefaultImagePath = "/Image/MyArticleLogo.jpg";

    }

    public enum SearchArticleKeywordType
    {
        Title,Author,User
    }

    public enum ResultSortType
    {
        ASC,DESC
    }

    public enum SearchResultSortColumn
    {
        ArticleId, CreatedOnDate, LastModifiedOnDate, ClickCount
    }
}