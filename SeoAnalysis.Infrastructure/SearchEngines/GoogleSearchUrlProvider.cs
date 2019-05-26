using SeoAnalysis.Core.SearchEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SeoAnalysis.Infrastructure.SearchEngines
{
    public class GoogleSearchUrlProvider : ISearchUrlProvider<GoogleResultsPageAnalyser>
    {

        #region " - - - - - - Fields - - - - - - "

        public const string GOOGLE_SEARCH_URL_BASE = "https://google.com.au/search";
        public const string MAX_RESULTS_QUERY_PARAMETER_NAME = "num";
        public const string SEARCH_QUERY_PARAMETER_NAME = "q";

        #endregion //Fields

        #region " - - - - - - ISearchUrlProvider Implementation - - - - - - "

        /// <summary>
        /// Generate a URL to be used by a Search Engine to perform a search.
        /// </summary>
        /// <param name="searchKeywords">The string of keywords to search.</param>
        /// <param name="numberOfResults">The number of results that the Search Engine should return.</param>
        /// <returns>A URI.</returns>
        public Uri CreateSearchUrl(string searchKeywords, int numberOfResults)
        {
            if (string.IsNullOrWhiteSpace(searchKeywords))
                throw new ArgumentException("The search keywords cannot be null or empty.", nameof(searchKeywords));
            if (numberOfResults <= 0)
                throw new ArgumentException("The number of results to return must be greater than 0.", nameof(numberOfResults));

            // Format the search terms into Google's format.
            var _GoogleSearchTerms = FormatSearchKeywords(searchKeywords);

            // Construct the URL Query String.
            var _QueryStringKeyValues = new Dictionary<string, string>
            {
                { SEARCH_QUERY_PARAMETER_NAME,      _GoogleSearchTerms },
                { MAX_RESULTS_QUERY_PARAMETER_NAME, numberOfResults.ToString() }
            };
            var _EscapedQueryString = string.Join('&', _QueryStringKeyValues.ToList().Select(kvp => $"{kvp.Key}={kvp.Value}"));

            // Construct the URI.
            return new Uri($"{GOOGLE_SEARCH_URL_BASE}?{_EscapedQueryString}");
        } //CreateSearchUrl

        #endregion //ISearchUrlProvider Implementation

        #region " - - - - - - Methods - - - - - - "

        /// <summary>
        /// Format the search terms into Google's format.
        /// </summary>
        /// <param name="searchKeywords">The search keywords string to format.</param>
        /// <returns>A formatted string suitable for use as URL query parameter value.</returns>
        private static string FormatSearchKeywords(string searchKeywords)
        {
            const string GOOGLE_SEARCH_TERM_DELIMITER = "+";
            const string WHITE_SPACE_REGEX = @"\s+|\t+";

            // Split the keywords on whitespace, removing all leading and trailing space beforehand.
            var _IndividualKeywords = Regex.Split(searchKeywords.Trim(), WHITE_SPACE_REGEX);

            // URL Encode the Keywords to preserve any special characters (i.e. '+').
            var _UrlEncodedKeywords = _IndividualKeywords.Select(kw => HttpUtility.UrlEncode(kw));

            // Join the Keywords with the search term delimiter.
            return string.Join(GOOGLE_SEARCH_TERM_DELIMITER, _UrlEncodedKeywords);
        } //FormatSearchKeywords

        #endregion //Methods

    } //GoogleSearchUrlProvider
}
