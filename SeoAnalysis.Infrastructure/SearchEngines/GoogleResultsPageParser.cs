using SeoAnalysis.Core.SearchEngines;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SeoAnalysis.Infrastructure.SearchEngines
{
    /// <summary>
    /// A Google Search Results Page HTML Parser.
    /// </summary>
    public class GoogleResultsPageParser : ISearchEngineResultsPageParser<GoogleResultsPageAnalyser>
    {

        #region " - - - - - - Fields - - - - - - "

        private const string SEARCH_RESULT_LINK_REGEX = @"[<]a[ ]href[=][\][""][/]url[?]q[=]([-a-zA-Z0-9@:%_\+.~#?&//=;]+)[\][""][>]";

        #endregion //Fields

        #region " - - - - - - ISearchEngineResultsPageParser Implementation - - - - - - "

        /// <summary>
        /// Parse HTML Content returned from a Search into a collection of SearchEngineResults.
        /// </summary>
        /// <param name="htmlContent">The HTML content containing a Search Engine Results Page.</param>
        /// <returns>A collection of SearchEngineResults.</returns>
        /// <remarks>
        /// A Google search result will be considered any hyperlink tag that contains a url with a prefix of \"/url?q=.
        /// For example: '<a href=\"/url?q=https://exampledomain.com/sample_path/\">'
        /// </remarks>
        public IEnumerable<SearchEngineResult> ParseHtmlSearchEngineResults(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent)) throw new ArgumentException("The HTML Content to parse cannot be null or empty.", nameof(htmlContent));

            var _SearchResults = new List<SearchEngineResult>();

            // Scan the HTML content for results.
            var _ResultMatches = Regex.Matches(htmlContent, SEARCH_RESULT_LINK_REGEX);

            // If there were no matches return an empty collection.
            if (_ResultMatches.Count == 0) return _SearchResults;

            // Iterate over the matches, adding them to the result set.
            for (int i = 0; i < _ResultMatches.Count; i++)
            {
                _SearchResults.Add(new SearchEngineResult
                {
                    Position = i + 1,
                    ResultLink = _ResultMatches[i].Groups[1].Value
                });
            }

            return _SearchResults;
        } //ParseHtmlSearchEngineResults

        #endregion //ISearchEngineResultsPageParser Implementation

    } //GoogleResultsPageParser
}
