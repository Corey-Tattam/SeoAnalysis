using Microsoft.Extensions.Logging;
using SeoAnalysis.Core.SearchEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeoAnalysis.Infrastructure.SearchEngines
{
    /// <summary>
    /// A Google Search Results Page HTML Parser.
    /// </summary>
    public class GoogleResultsPageParser : ISearchEngineResultsPageParser<GoogleResultsPageAnalyser>
    {

        #region " - - - - - - Fields - - - - - - "

        private const string MAIN_RESULT_REGEX_PREFIX = @"[<]div[ ]class[=][\][""][A-Za-z0-9 ]+[\][""][>]";
        private const string SEARCH_RESULT_LINK_REGEX = @"[<]a[ ]href[=][\][""][/]url[?]q[=]([-a-zA-Z0-9@:%_\+.~#?&//=;]+)[\][""][>]";

        private readonly ILogger m_Logger;

        #endregion //Fields

        #region " - - - - - - Constructors - - - - - - "

        public GoogleResultsPageParser(ILogger logger)
        {
            this.m_Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion //Constructors

        #region " - - - - - - ISearchEngineResultsPageParser Implementation - - - - - - "

        /// <summary>
        /// Parse HTML Content returned from a Search into a collection of SearchEngineResults.
        /// </summary>
        /// <param name="htmlContent">The HTML content containing a Search Engine Results Page.</param>
        /// <returns>A collection of SearchEngineResults.</returns>
        /// <remarks>
        /// A Google search result will be considered any hyperlink tag that contains a url with a prefix of \"/url?q=.
        /// For example: '<a href=\"/url?q=https://exampledomain.com/sample_path/\">'.
        /// 
        /// To filter out sub-results, the above tag needs to be preceded by a div tag. For example:
        ///  - Main Result: <div class=\"Xydfse\"><a href....\">
        ///  - Sub-Result: <span class=\"BNawe\"><a href....\">
        /// 
        /// Filtering out sub-results means that only the visible green links are counted as results."
        /// </remarks>
        public IEnumerable<SearchEngineResult> ParseHtmlSearchEngineResults(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent)) throw new ArgumentException("The HTML Content to parse cannot be null or empty.", nameof(htmlContent));

            var _SearchResults = new List<SearchEngineResult>();

            // Scan the HTML content for results.
            var _MainResultRegex = $"{MAIN_RESULT_REGEX_PREFIX}{SEARCH_RESULT_LINK_REGEX}";
            var _ResultMatches = Regex.Matches(htmlContent, _MainResultRegex);
            this.m_Logger.LogDebug($"{_ResultMatches.Count} main results found.");

            // If there were no matches return an empty collection.
            if (!_ResultMatches.Any()) return _SearchResults;

            // Iterate over the matches, adding them to the result set.
            const int REGEX_RESULT_CAPTURE_INDEX = 1;
            for (int i = 0; i < _ResultMatches.Count; i++)
            {
                _SearchResults.Add(new SearchEngineResult
                {
                    Position = i + 1,
                    ResultLink = _ResultMatches[i].Groups[REGEX_RESULT_CAPTURE_INDEX].Value
                });
            }

            return _SearchResults;
        } //ParseHtmlSearchEngineResults

        #endregion //ISearchEngineResultsPageParser Implementation

    } //GoogleResultsPageParser
}
