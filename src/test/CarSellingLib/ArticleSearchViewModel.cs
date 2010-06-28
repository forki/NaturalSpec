using System.Collections.Generic;
using Microsoft.FSharp.Collections;

namespace xUnit.BDDExtensions.SampleCode.UI
{
    public class ArticleSearchViewModel
    {
        private readonly IArticleSource _articleSource;

        public ArticleSearchViewModel(IArticleSource articleSource)
        {
            _articleSource = articleSource;
        }

        public string SearchTerm { get; set; }

        public FSharpList<Article> Results { get; set; }

        public void StartSearch()
        {
            Results = _articleSource.Search(SearchTerm);
        }
    }
}