﻿using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;

namespace MyArticle
{
    public class MyArticleItem : IHydratable
    {

         public MyArticleItem()
        {
            ArticleContentItem = new ContentItem();
        }

        public ContentItem ArticleContentItem{get;set; }

        public bool IsComment { get; set; }

        public bool IsPublished { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public int ClickCount { get; set; }

        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int PortalId { get; set; }

        public int CreatedByUserId { get; set; }

        public int LastModifiedByUserId { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public int ContentItemId { get; set; }

        public int TotalRecords { get; set; }

        public string Author
        {
            get; set;
        }


        public int KeyID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Fill(System.Data.IDataReader dr)
        {
            try
            {
                ArticleContentItem = ContentController.Instance.GetContentItem(Null.SetNullInteger(dr["ContentItemId"]));
                ContentItemId = Null.SetNullInteger(dr["ContentItemId"]);
                ArticleId = Null.SetNullInteger(dr["ArticleId"]);
                Title = Null.SetNullString(dr["Title"]);
                ClickCount = Null.SetNullInteger(dr["ClickCount"]);
                IsPublished = Convert.ToBoolean(Null.SetNullInteger(dr["IsPublished"]));
                IsComment = Convert.ToBoolean(Null.SetNullInteger(dr["IsComment"]));
                Body = Null.SetNullString(dr["Body"]);
                CreatedByUserId = Null.SetNullInteger(dr["CreatedByUserId"]);
                LastModifiedByUserId = Null.SetNullInteger(dr["LastModifiedByUserId"]);
                CreatedOnDate = Null.SetNullDateTime(dr["CreatedOnDate"]);
                LastModifiedOnDate = Null.SetNullDateTime(dr["LastModifiedOnDate"]);
                Description = Null.SetNullString(dr["Description"]);
                Author = Null.SetNullString(dr["Author"]);
                ThumbnailUrl = Null.SetNullString(dr["ThumbnailUrl"]);
                TotalRecords = Null.SetNullInteger(dr["TotalRecords"]);
            }
            catch (Exception e)
            {

            }
        }

    }
   
    public static class ArticleListDisplayStyle
    {
        public static List<string> Styles
        {
            get
            {
                List<string> _Styles = new List<string>();
                _Styles.Add("Title");
                _Styles.Add("Title And Image");
                return _Styles;
            }
        }
    }

}
