using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Common;

namespace MyArticle
{
    ///<summary>
    /// The content class used for creating and maintaining content items
    ///</summary>
    public class MyArticleContent
    {

        private const string ContentTypeName = "MyArticle";




        /// <summary>
        /// This should only run after the ArticleItem exists in the data store. 
        /// </summary>
        /// <returns>The newly created ContentItemID from the data store.</returns>
        public ContentItem CreateContentItem(MyArticleItem a, int tabId)
        {
            var typeController = new ContentTypeController();
            var colContentTypes = (from t in typeController.GetContentTypes() where t.ContentType == ContentTypeName select t);
            int contentTypeId;

            if (colContentTypes.Count() > 0)
            {
                var contentType = colContentTypes.Single();
                contentTypeId = contentType == null ? CreateContentType() : contentType.ContentTypeId;
            }
            else
            {
                contentTypeId = CreateContentType();
            }

            var objContent = new ContentItem
            {
                Content =  HttpUtility.HtmlDecode(a.Title),
                ContentTypeId = contentTypeId,
                Indexed = false,
                ContentKey = "aid=" + a.ArticleId,
           //     ModuleID = a.ModuleID,
                TabID = tabId
            };

            objContent.ContentItemId = Util.GetContentController().AddContentItem(objContent);

            // Add Terms
            var cntTerm = new MyArticleTrems();
            cntTerm.ManageArticleTerms(a, objContent);

            return objContent;
        }

        /// <summary>
        /// This is used to update the content in the ContentItems table. Should be called when an ArticleItem is updated.
        /// </summary>
        public void UpdateContentItem(MyArticleItem a, int tabId)
        {

            var objContent = Util.GetContentController().GetContentItem(a.ContentItemId);

            if (objContent == null) return;
            objContent.Content =  HttpUtility.HtmlDecode(a.Title);
            objContent.TabID = tabId;


            Util.GetContentController().UpdateContentItem(objContent);

            // Update Terms
            var cntTerm = new MyArticleTrems();
            cntTerm.ManageArticleTerms(a, objContent);
        }

        /// <summary>
        /// This removes a content item associated with an article from the data store. Should run every time an article is deleted.
        /// </summary>
        /// <param name="a">The Content Item we wish to remove from the data store.</param>
        public void DeleteContentItem(MyArticleItem a)
        {
            if (a.ContentItemId <= Null.NullInteger) return;
            var objContent = Util.GetContentController().GetContentItem(a.ContentItemId);
            if (objContent == null) return;

            // remove any metadata/terms associated first (perhaps we should just rely on ContentItem cascade delete here?)
            var cntTerms = new MyArticleTrems();
        //    cntTerms.RemoveArticleTerms(a);

            Util.GetContentController().DeleteContentItem(objContent);
        }

        #region Private Methods

        /// <summary>
        /// Creates a Content Type (for taxonomy) in the data store.
        /// </summary>
        /// <returns>The primary key value of the new ContentType.</returns>
        private static int CreateContentType()
        {
            var typeController = new ContentTypeController();
            var objContentType = new ContentType { ContentType = ContentTypeName };

            return typeController.AddContentType(objContentType);
        }

        #endregion

    }
}


