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
                TabID = tabId
            };

            objContent.ContentItemId = Util.GetContentController().AddContentItem(objContent);

            var cntTerm = new MyArticleTrems();
            cntTerm.ManageArticleTerms(a, objContent);

            return objContent;
        }


        public void UpdateContentItem(MyArticleItem a, int tabId)
        {

            var objContent = Util.GetContentController().GetContentItem(a.ContentItemId);

            if (objContent == null) return;
            objContent.Content =  HttpUtility.HtmlDecode(a.Title);
            objContent.TabID = tabId;


            Util.GetContentController().UpdateContentItem(objContent);
            
            var cntTerm = new MyArticleTrems();
            cntTerm.ManageArticleTerms(a, objContent);
        }


        public void DeleteContentItem(MyArticleItem a)
        {
            if (a.ContentItemId <= Null.NullInteger) return;
            var objContent = Util.GetContentController().GetContentItem(a.ContentItemId);
            if (objContent == null) return;

          
            var cntTerms = new MyArticleTrems();
   

            Util.GetContentController().DeleteContentItem(objContent);
        }




        private static int CreateContentType()
        {
            var typeController = new ContentTypeController();
            var objContentType = new ContentType { ContentType = ContentTypeName };

            return typeController.AddContentType(objContentType);
        }



    }
}


