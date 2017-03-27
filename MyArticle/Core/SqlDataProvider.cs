
using System;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Entities.Portals;

namespace MyArticle
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider
    {


        private static SqlDataProvider _provider;

        // return the provider
        ///<summary>
        /// Gets an instance of the dataprovider object for use
        ///</summary>
        ///<returns></returns>
        public static SqlDataProvider Instance()
        {
            if (_provider == null)
            {
                const string assembly = "MyArticle.SqlDataprovider,MyArticle";
                Type objectType = Type.GetType(assembly, true, true);

                if (objectType != null)
                {
                    _provider = (SqlDataProvider)Activator.CreateInstance(objectType);
                    DataCache.SetCache(objectType.FullName, _provider);
                }
            }

            return _provider;
        }

        public string ConnectionString
        {
            get
            {
                return Config.GetConnectionString();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IDataReader GetArticleById(int articleId)
        {

            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticle", new SqlParameter("@ArticleId", articleId));
        }


        public IDataReader GetArticlesByUser(int pageSize, int pageIndex, int portalId, int sortAsc, int userId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByUser",

               new SqlParameter("@PageSize", pageSize),
               new SqlParameter("@PageIndex", pageIndex),
               new SqlParameter("@PortalId", portalId),
               new SqlParameter("@SortAsc", sortAsc),
               new SqlParameter("@UserId", userId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public IDataReader GetArticlesByTag(int pageSize, int pageIndex, int portalId, int sortAsc, string tagName, bool published)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByTag",

                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                new SqlParameter("@SortAsc", sortAsc),
                new SqlParameter("@TagName", tagName),
                new SqlParameter("@IsPublished", published));
        }

        public IDataReader GetArticles(int pageSize, int pageIndex, int portalId, int sortAsc)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticles",

                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                new SqlParameter("@SortAsc", sortAsc));
        }

        public IDataReader SearchArticlesByTitle(int pageSize, int pageIndex, int portalId, int sortAsc, string title)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spSearchArticles",

               new SqlParameter("@PageSize", pageSize),
               new SqlParameter("@PageIndex", pageIndex),
               new SqlParameter("@PortalId", portalId),
               new SqlParameter("@SortAsc", sortAsc),
               new SqlParameter("@Title", title));
        }


        public IDataReader SearchArticlesByAuthor(int pageSize, int pageIndex, int portalId, int sortAsc, string author)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spSearchArticles",

               new SqlParameter("@PageSize", pageSize),
               new SqlParameter("@PageIndex", pageIndex),
               new SqlParameter("@PortalId", portalId),
               new SqlParameter("@SortAsc", sortAsc),
               new SqlParameter("@Author", author));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int AddArticle(MyArticleItem a)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, "MyArticle_spAddArticle"
                , new SqlParameter("@PortalId", a.PortalId)
                , new SqlParameter("@Title", a.Title)
                , new SqlParameter("@IsPublished", a.IsPublished)
                , new SqlParameter("@Body", a.Body)
                , new SqlParameter("@CreatedByUserId", a.CreatedByUserId)
                , new SqlParameter("@Description", a.Description)
                , new SqlParameter("@CreatedOnDate", a.CreatedOnDate)
                , new SqlParameter("@LastModifiedOnDate", a.LastModifiedOnDate)
                , new SqlParameter("@ThumbnailUrl", a.ThumbnailUrl)
                , new SqlParameter("LastModifiedByUserId", a.LastModifiedByUserId)
                , new SqlParameter("Author", a.Author)
                , new SqlParameter("IsComment", a.IsComment)
                ));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        public void DeleteArticle(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spDeleteArticle", new SqlParameter("@ArticleId", articleId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        public void UpdateArticle(MyArticleItem a)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spUpdateArticle"
                , new SqlParameter("@ArticleId", a.ArticleId)
                , new SqlParameter("@PortalId", a.PortalId)
               , new SqlParameter("@Title", a.Title)
               , new SqlParameter("@IsPublished", a.IsPublished)
               , new SqlParameter("@Body", a.Body)
               , new SqlParameter("@LastModifiedOnDate", a.LastModifiedOnDate)
               , new SqlParameter("@LastModifiedByUserId", a.LastModifiedByUserId)
               , new SqlParameter("@ContentItemId", a.ContentItemId)
                , new SqlParameter("@Description", a.Description)
                , new SqlParameter("@ThumbnailUrl", a.ThumbnailUrl)
                , new SqlParameter("Author", a.Author)
                , new SqlParameter("IsComment", a.IsComment)
               );
        }



        public void AddClickCount(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spUpdateClickCount"
              , new SqlParameter("@ArticleId", articleId)

             );
        }

        public int GetArticlesCount(int portalId)
        {
            //return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "spGetArticlesCount"
            //   , new SqlParameter("@PortalId", portalId)

            //  );

            return (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, "SELECT COUNT(*) FROM MyArticle WHERE   PortalId = " + portalId.ToString());


        }

    }

}