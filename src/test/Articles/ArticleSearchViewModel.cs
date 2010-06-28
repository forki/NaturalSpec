using System.Collections.Generic;

namespace Articles
{
    public class ArticleSearchViewModel
    {
        private readonly IArticleSource _articleSource;

        public ArticleSearchViewModel(IArticleSource articleSource)
        {
            _articleSource = articleSource;
        }

        public string SearchTerm { get; set; }

        public List<Article> Results { get; set; }

        public void StartSearch()
        {
            Results = _articleSource.Search(SearchTerm);
        }
    }
}