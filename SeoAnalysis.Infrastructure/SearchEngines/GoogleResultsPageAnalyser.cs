using SeoAnalysis.Core.Net;
using SeoAnalysis.Core.SearchEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeoAnalysis.Infrastructure.SearchEngines
{
    public class GoogleResultsPageAnalyser : ISearchEngineResultsPageAnalyser
    {

        #region " - - - - - - Fields - - - - - - "

        private const int NUM_SEARCH_RESULTS = 100;

        private readonly IHttpRequestSender m_HttpRequestSender;
        private readonly ISearchEngineResultsPageParser<GoogleResultsPageAnalyser> m_SearchResultParser;
        private readonly ISearchUrlProvider<GoogleResultsPageAnalyser> m_SearchUrlProvider;

        #endregion //Fields

        #region " - - - - - - Constructors - - - - - - "

        public GoogleResultsPageAnalyser
        (
            IHttpRequestSender httpRequestSender, 
            ISearchEngineResultsPageParser<GoogleResultsPageAnalyser> searchResultParser,
            ISearchUrlProvider<GoogleResultsPageAnalyser> searchUrlProvider
        )
        {
            this.m_HttpRequestSender    = httpRequestSender  ?? throw new ArgumentNullException(nameof(httpRequestSender));
            this.m_SearchResultParser   = searchResultParser ?? throw new ArgumentNullException(nameof(searchResultParser));
            this.m_SearchUrlProvider    = searchUrlProvider  ?? throw new ArgumentNullException(nameof(searchUrlProvider));
        }

        #endregion //Constructors

        #region " - - - - - - ISearchEngineResultsPageAnalyser Implementation - - - - - - "

        /// <summary>
        /// Get the positions of a domain in a Search Engine Results Page from a keyword search.
        /// </summary>
        /// <param name="keywords">The keywords to search.</param>
        /// <param name="domain">The domain to match.</param>
        /// <returns>The position numbers of all matches in the results.</returns>
        public async Task<IEnumerable<int>> GetSearchEngineResultsPagePositions(string keywords, string domain)
        {
            if (string.IsNullOrWhiteSpace(keywords))    throw new ArgumentException("At least one keyword must be specified.", nameof(keywords));
            if (string.IsNullOrWhiteSpace(domain))      throw new ArgumentException("The domain must be specified.", nameof(domain));

            // Create the Search URL.
            var _SearchUrl = this.m_SearchUrlProvider.CreateSearchUrl(keywords, NUM_SEARCH_RESULTS);

            // Send the Request.
            var _SearchHtmlContent = await this.m_HttpRequestSender.GetStringContentAsync(_SearchUrl);

            // Parse HTML Content as Search Results.
            var _SearchResults = this.m_SearchResultParser.ParseHtmlSearchEngineResults(_SearchHtmlContent);

            // Extract relevant result positions and return.
            return _SearchResults.Where(s => s.ResultLink.Contains(domain)).Select(s => s.Position);
        } //GetSearchEngineResultsPagePositions

        #endregion //ISearchEngineResultsPageAnalyser Implementation

    } //GoogleResultsPageAnalyser
}
