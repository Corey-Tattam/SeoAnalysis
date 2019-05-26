using SeoAnalysis.Core.Exceptions;
using SeoAnalysis.Core.Models;
using SeoAnalysis.Core.SearchEngines;
using SeoAnalysis.Core.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeoAnalysis.Infrastructure.Services
{
    public class SeoAnalysisReportService : ISeoAnalysisReportService
    {

        #region " - - - - - - Fields - - - - - - "

        private readonly ISearchEngineResultsPageAnalyser m_SearchResultsPageAnalyser;

        #endregion //Fields

        #region " - - - - - - Constructors - - - - - - "

        public SeoAnalysisReportService(ISearchEngineResultsPageAnalyser searchResultsPageAnalyser)
        {
            this.m_SearchResultsPageAnalyser = searchResultsPageAnalyser;
        }

        #endregion //Constructors

        #region " - - - - - - ISeoAnalysisReportService Implementation - - - - - - "

        /// <summary>
        /// Given a keywords string and URL, retrieve an S.E.O. Report. 
        /// </summary>
        /// <param name="keywords">The keywords to search.</param>
        /// <param name="url">The URL to match.</param>
        /// <returns>An SeoAnalysisReport.</returns>
        public async Task<SeoAnalysisReport> GetSeoAnalysisReport(string keywords, string url)
        {
            //Validate parameters
            ValidateSearchKeywords(keywords);
            ValidateUrl(url);

            // Sanitise parameters.
            var _SanitisedUrl = SanitiseUrl(url);

            // Perform the search.
            var _SearchResults = await this.m_SearchResultsPageAnalyser.GetSearchEngineResultsPagePositions(keywords, url);

            // Return the report.
            return new SeoAnalysisReport { KeywordSearchResultsPositions = _SearchResults };
        } //GetSeoReport

        #endregion //ISeoAnalysisReportService Implementation

        #region " - - - - - - Methods - - - - - - "

        /// <summary>
        /// Sanitise the URL.
        /// </summary>
        private static string SanitiseUrl(string url)
        {
            var _SanitisedUrl = url.Trim();

            // Remove any URI Scheme that may have been included.
            const string URI_SCHEME_REGEX = ".*[:][/][/]";
            _SanitisedUrl = Regex.Replace(_SanitisedUrl, URI_SCHEME_REGEX, string.Empty);

            return _SanitisedUrl;
        } //SanitiseUrl

        /// <summary>
        /// Validate the Search Keywords.
        /// </summary>
        private static void ValidateSearchKeywords(string keywords)
        {
            if (string.IsNullOrWhiteSpace(keywords))
                throw new ValidationException("At least one Keyword must be specified.");
        } //ValidateSearchKeywords

        /// <summary>
        /// Validate the URL to match.
        /// </summary>
        private static void ValidateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ValidationException("At URL must be specified.");
        } //ValidateUrl

        #endregion //Methods

    } //SeoAnalysisReportService
}
