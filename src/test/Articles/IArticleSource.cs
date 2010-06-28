using Microsoft.FSharp.Collections;

namespace Articles
{
    public interface IArticleSource
    {
        FSharpList<Article> Search(string searchTerm);
    }
}