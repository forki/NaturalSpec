using System.Collections.Generic;
using Microsoft.FSharp.Collections;

namespace xUnit.BDDExtensions.SampleCode.UI
{
    public interface IArticleSource
    {
        FSharpList<Article> Search(string searchTerm);
    }
}