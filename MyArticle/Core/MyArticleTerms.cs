using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Content;


namespace MyArticle
{


    public class MyArticleTrems
    {

     
        public void ManageArticleTerms(MyArticleItem a, ContentItem objContent)
        {
            RemoveArticleTerms(objContent);

            foreach (var term in a.ArticleContentItem.Terms)
            {
                Util.GetTermController().AddTermToContent(term, objContent);
            }
        }

        public void RemoveArticleTerms(ContentItem objContent)
        {
            Util.GetTermController().RemoveTermsFromContent(objContent);
        }
    }
}