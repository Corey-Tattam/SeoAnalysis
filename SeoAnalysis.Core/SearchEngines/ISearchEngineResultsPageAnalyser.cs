using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeoAnalysis.Core.SearchEngines
{
    public interface ISearchEngineResultsPageAnalyser
    {

        /// <summary>
        /// Get the positions of a domain in a Search Engine Results Page from a keyword search.
        /// </summary>
        /// <param name="keywords">The keywords to search.</param>
        /// <param name="domain">The domain to match.</param>
        /// <returns>The position numbers of all matches in the results.</returns>
        Task<IEnumerable<int>> GetSearchEngineResultsPagePositions(string keywords, string domain);

    } //ISearchEngineResultsPageAnalyser
}
