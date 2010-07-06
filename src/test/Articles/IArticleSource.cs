using System.Collections.Generic;

namespace Articles
{
    public interface IArticleSource
    {
        List<Article> Search(string searchTerm);
    }
}