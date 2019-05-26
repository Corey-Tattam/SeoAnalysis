using SeoAnalysis.Core.Net;
using SeoAnalysis.Core.SearchEngines;
using SeoAnalysis.Infrastructure.SearchEngines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SeoAnalysis.Infrastructure.Tests.Unit.SearchEngines
{
    public class GoogleResultsPageAnalyserTests
    {

        #region " - - - - - - GetSearchEngineResultsPagePositions Tests - - - - - - "

        [Fact]
        public async Task GetSearchEngineResultsPagePositions_ValidArguments_Success()
        {
            // Arrange
            const string DOMAIN_TO_MATCH = "domaintomatch.com";
            const int RESULT_POSITION_1 = 2;
            var _SearchResults = new SearchEngineResult[]
            {
                new SearchEngineResult { Position = 1,                  ResultLink = "test@1.com" },
                new SearchEngineResult { Position = RESULT_POSITION_1,  ResultLink = DOMAIN_TO_MATCH },
                new SearchEngineResult { Position = 3,                  ResultLink = "test@3.com" },
            };

            var _HttpRequestSender      = new TestHttpRequestSender("Some Content");
            var _SearchResultsParser    = new TestGoogleResultsPageParser(_SearchResults);
            var _SearchUrlProvider      = new TestGoogleSearchUrlProvider(new Uri("https://www.google.com"));
            var _Analyser               = new GoogleResultsPageAnalyser(_HttpRequestSender, _SearchResultsParser, _SearchUrlProvider);

            // Act
            var _Results = await _Analyser.GetSearchEngineResultsPagePositions("random keywords", DOMAIN_TO_MATCH);

            // Assert
            Assert.Collection(_Results, p => Assert.Equal(RESULT_POSITION_1, p));
        } //GetSearchEngineResultsPagePositions_ValidArguments_Success


        // Supporting Functionality ------------------------------------------------------

        private class TestHttpRequestSender : IHttpRequestSender
        {

            #region " - - - - - - Fields - - - - - - "

            private readonly string m_ContentToReturn;

            #endregion //Fields

            #region " - - - - - - Constructors - - - - - - "

            public TestHttpRequestSender(string contentToReturn)
            {
                this.m_ContentToReturn = contentToReturn;
            }

            #endregion //Constructors

            #region " - - - - - - IHttpRequestSender Implementation - - - - - - "

            public async Task<string> GetStringContentAsync(Uri requestUri) => await Task.FromResult(this.m_ContentToReturn);

            #endregion //IHttpRequestSender Implementation

        } //TestHttpRequestSender

        private class TestGoogleResultsPageParser : ISearchEngineResultsPageParser<GoogleResultsPageAnalyser>
        {

            #region " - - - - - - Fields - - - - - - "

            private readonly IEnumerable<SearchEngineResult> m_ResultsToReturn;

            #endregion //Fields

            #region " - - - - - - Constructors - - - - - - "

            public TestGoogleResultsPageParser(IEnumerable<SearchEngineResult> resultsToReturn)
            {
                this.m_ResultsToReturn = resultsToReturn;
            }

            #endregion //Constructors

            #region " - - - - - - ISearchEngineResultsPageParser Implentation - - - - - - "

            public IEnumerable<SearchEngineResult> ParseHtmlSearchEngineResults(string htmlContent) => this.m_ResultsToReturn;

            #endregion //ISearchEngineResultsPageParser Implentation

        } //TestGoogleResultsPageParser

        private class TestGoogleSearchUrlProvider : ISearchUrlProvider<GoogleResultsPageAnalyser>
        {

            #region " - - - - - - Fields - - - - - - "

            private readonly Uri m_UriToReturn;

            #endregion //Fields

            #region " - - - - - - Constructors - - - - - - "

            public TestGoogleSearchUrlProvider(Uri uriToReturn)
            {
                this.m_UriToReturn = uriToReturn;
            }

            #endregion //Constructors

            #region " - - - - - - ISearchUrlProvider Implementation - - - - - - "

            public Uri CreateSearchUrl(string searchKeywords, int numberOfResults) => this.m_UriToReturn;

            #endregion //ISearchUrlProvider Implementation

        } //TestGoogleSearchUrlProvider

        #endregion //GetSearchEngineResultsPagePositions Tests

    } //GoogleResultsPageAnalyserTests
}
