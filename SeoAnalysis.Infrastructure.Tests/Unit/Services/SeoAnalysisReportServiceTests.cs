using SeoAnalysis.Core.Exceptions;
using SeoAnalysis.Core.SearchEngines;
using SeoAnalysis.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SeoAnalysis.Infrastructure.Tests.Unit.Services
{
    public class SeoAnalysisReportServiceTests
    {

        #region " - - - - - - GetSeoReport Tests - - - - - - "

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetSeoReport_InvalidKeywordsArgument_ThrowsValidationException(string keywords)
        {
            // Arrange
            var _ResultsPageAnalyser = new TestSearchEngineAnalyser(Enumerable.Empty<int>());
            var _SeoReportService = new SeoAnalysisReportService(_ResultsPageAnalyser);

            // Act
            var _Exception = await Record.ExceptionAsync(() => _SeoReportService.GetSeoAnalysisReport(keywords, "test@test.com"));

            // Assert
            Assert.NotNull(_Exception);
            Assert.IsType<ValidationException>(_Exception);
        } //GetSeoReport_InvalidKeywordsArgument_ThrowsValidationException

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetSeoReport_InvalidUrlArgument_ThrowsValidationException(string url)
        {
            // Arrange
            var _ResultsPageAnalyser = new TestSearchEngineAnalyser(Enumerable.Empty<int>());
            var _SeoReportService = new SeoAnalysisReportService(_ResultsPageAnalyser);

            // Act
            var _Exception = await Record.ExceptionAsync(() => _SeoReportService.GetSeoAnalysisReport("key words", url));

            // Assert
            Assert.NotNull(_Exception);
            Assert.IsType<ValidationException>(_Exception);
        } //GetSeoReport_InvalidKeywordsArgument_ThrowsValidationException

        [Fact]
        public async Task GetSeoReport_ValidInput_Success()
        {
            // Arrange
            var _ResultPositions = new int[] { 1, 33, 64 };
            var _ResultsPageAnalyser = new TestSearchEngineAnalyser(_ResultPositions);
            var _SeoReportService = new SeoAnalysisReportService(_ResultsPageAnalyser);

            // Act
            var _SeoReportResult = await _SeoReportService.GetSeoAnalysisReport("key words", "test@test.com");

            // Assert
            Assert.Equal(_ResultPositions, _SeoReportResult.KeywordSearchResultsPositions);
        } //GetSeoReport_ValidInput_Success


        // Supporting Functionality ------------------------------------------------------

        private class TestSearchEngineAnalyser : ISearchEngineResultsPageAnalyser
        {

            #region " - - - - - - Fields - - - - - - "

            private readonly IEnumerable<int> m_ResultPositionsToReturn;

            #endregion //Fields

            #region " - - - - - - Constructors - - - - - - "

            public TestSearchEngineAnalyser(IEnumerable<int> resultPositionsToReturn)
            {
                this.m_ResultPositionsToReturn = resultPositionsToReturn;
            }

            #endregion //Constructors

            #region " - - - - - - ISearchEngineResultsPageAnalyser Implementation - - - - - - "

            public Task<IEnumerable<int>> GetSearchEngineResultsPagePositions(string keywords, string domain) => Task.FromResult(this.m_ResultPositionsToReturn);

            #endregion //ISearchEngineResultsPageAnalyser Implementation

        } //TestSearchEngineAnalyser

        #endregion //GetSeoReport Tests

    } //SeoAnalysisReportServiceTests
}
