using System.Collections.Generic;

namespace SeoAnalysis.Core.SearchEngines
{
    public interface ISearchEngineResultsPageParser<T> where T : ISearchEngineResultsPageAnalyser
    {

        /// <summary>
        /// Parse HTML Content returned from a Search into a collection of SearchEngineResults.
        /// </summary>
        /// <param name="htmlContent">The HTML content containing a Search Engine Results Page.</param>
        /// <returns>A collection of SearchEngineResults.</returns>
        IEnumerable<SearchEngineResult> ParseHtmlSearchEngineResults(string htmlContent);

    } //ISearchEngineResultsPageParser
}
