using SeoAnalysis.Infrastructure.SearchEngines;
using System;
using Xunit;

namespace SeoAnalysis.Infrastructure.Tests.Unit.SearchEngines
{
    public class GoogleSearchUrlProviderTests
    {

        #region " - - - - - - CreateSearchUrl Tests - - - - - - "

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void CreateSearchUrl_InvalidNumberOfResultsArgument_ThrowsArgumentException(int numberOfResults)
        {
            // Arrange
            var _SearchUrlProvider = new GoogleSearchUrlProvider();

            // Act
            var _Exception = Record.Exception(() => _SearchUrlProvider.CreateSearchUrl("keywords", numberOfResults));

            // Assert
            Assert.NotNull(_Exception);
            Assert.IsType<ArgumentException>(_Exception);
        } //CreateSearchUrl_InvalidNumberOfResultsArgument_ThrowsArgumentException

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateSearchUrl_InvalidKeywordsArgument_ThrowsArgumentException(string keywords)
        {
            // Arrange
            var _SearchUrlProvider = new GoogleSearchUrlProvider();

            // Act
            var _Exception = Record.Exception(() => _SearchUrlProvider.CreateSearchUrl(keywords, 1));

            // Assert
            Assert.NotNull(_Exception);
            Assert.IsType<ArgumentException>(_Exception);
        } //CreateSearchUrl_InvalidKeywordsArgument_ThrowsArgumentException

        [Theory]
        [InlineData("  LeadingWhitespace",          "LeadingWhitespace")]
        [InlineData("TrailingWhitespace  ",         "TrailingWhitespace")]
        [InlineData("White Space",                  "White+Space")]
        [InlineData("Running    Whitespace",        "Running+Whitespace")]
        [InlineData("  Lots   of    whitespace  ",  "Lots+of+whitespace")]
        public void CreateSearchUrl_KeywordStringWithRunningSpaces_MatchExpectedQueryStringValue(string keywords, string expectedQueryStringValue)
        {
            // Arrange
            var _SearchUrlProvider = new GoogleSearchUrlProvider();

            // Act
            var _SearchUrl = _SearchUrlProvider.CreateSearchUrl(keywords, 10);

            // Assert
            Assert.Contains(expectedQueryStringValue, _SearchUrl.Query);
        } //CreateSearchUrl_KeywordStringWithRunningSpaces_MatchExpectedQueryStringValue

        [Theory]
        [InlineData("plus+",    "plus%2b")]
        [InlineData("plus +",   "plus+%2b")]
        public void CreateSearchUrl_KeywordStringWithSpecialCharacters_MatchExpectedQueryStringValue(string keywords, string expectedQueryStringValue)
        {
            // Arrange
            var _SearchUrlProvider = new GoogleSearchUrlProvider();

            // Act
            var _SearchUrl = _SearchUrlProvider.CreateSearchUrl(keywords, 10);

            // Assert
            Assert.Contains(expectedQueryStringValue, _SearchUrl.Query);
        } //CreateSearchUrl_KeywordStringWithSpecialCharacters_MatchExpectedQueryStringValue

        [Fact]
        public void CreateSearchUrl_ValidInput_SuccessfulCreation()
        {
            // Arrange
            var _SearchUrlProvider = new GoogleSearchUrlProvider();
            var _Keywords = "key words";
            var _NumberOfResults = 10;

            var _BaseUrl = GoogleSearchUrlProvider.GOOGLE_SEARCH_URL_BASE;
            var _SearchParameterName = GoogleSearchUrlProvider.SEARCH_QUERY_PARAMETER_NAME;
            var _MaxResultsParameterName = GoogleSearchUrlProvider.MAX_RESULTS_QUERY_PARAMETER_NAME;
            var _ExpectedResult = new Uri($"{_BaseUrl}?{_SearchParameterName}=key+words&{_MaxResultsParameterName}={_NumberOfResults}");

            // Act
            var _SearchUrl = _SearchUrlProvider.CreateSearchUrl(_Keywords, _NumberOfResults);

            // Assert
            Assert.Equal(_SearchUrl, _ExpectedResult);
        } //CreateSearchUrl_ValidInput_SuccessfulCreation

        #endregion //CreateSearchUrl Tests

    } //GoogleSearchUrlProviderTests
}
