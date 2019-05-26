using System;

namespace SeoAnalysis.Core.SearchEngines
{
    public interface ISearchUrlProvider<T> where T : ISearchEngineResultsPageAnalyser
    {

        /// <summary>
        /// Generate a URL to be used by a Search Engine to perform a search.
        /// </summary>
        /// <param name="searchKeywords">The string of keywords to search.</param>
        /// <param name="numberOfResults">The number of results that the Search Engine should return.</param>
        /// <returns>A URI.</returns>
        Uri CreateSearchUrl(string searchKeywords, int numberOfResults);

    } //ISearchUrlProvider
}
