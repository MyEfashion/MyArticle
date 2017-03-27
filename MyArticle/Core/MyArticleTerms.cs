using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Content;


namespace MyArticle
{

    ///<summary>
    /// The terms class used for managing categories for Articles
    ///</summary>
    public class MyArticleTrems
    {

        ///<summary>
        /// This should run only after the ArticleItem has been added/updated in data store and the ContentItem exists.
        ///</summary>
        ///<param name="a"></param>
        ///<param name="objContent"></param>
        public void ManageArticleTerms(MyArticleItem a, ContentItem objContent)
        {
            RemoveArticleTerms(objContent);

            foreach (var term in a.ArticleContentItem.Terms)
            {
                Util.GetTermController().AddTermToContent(term, objContent);
            }
        }

        /// <summary>
        /// Removes terms associated w/ a specific ContentItem.
        /// </summary>
        /// <param name="objContent"></param>
        public void RemoveArticleTerms(ContentItem objContent)
        {
            Util.GetTermController().RemoveTermsFromContent(objContent);
        }
    }
}