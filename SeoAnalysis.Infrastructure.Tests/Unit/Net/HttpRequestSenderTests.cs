using SeoAnalysis.Infrastructure.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SeoAnalysis.Infrastructure.Tests.Unit.Net
{
    public class HttpRequestSenderTests
    {

        #region " - - - - - - Search Tests - - - - - - "

        [Fact]
        public async Task GetStringContentAsync_UnsuccessfulResponse_ThrowsHttpRequestException()
        {
            // Arrange
            var _HttpClient = new HttpClient();
            var _RequestSender = new HttpRequestSender(_HttpClient);
            var _Uri = new Uri("https://httpstat.us/400");

            // Act
            var _Exception = await Record.ExceptionAsync(() => _RequestSender.GetStringContentAsync(_Uri));

            // Assert
            Assert.NotNull(_Exception);
            Assert.IsType<HttpRequestException>(_Exception);
        } //GetStringContentAsync_UnsuccessfulResponse_ThrowsHttpRequestException

        [Fact]
        public async Task GetStringContentAsync_ValidRequestUri_ReturnContent()
        {
            // Arrange
            var _HttpClient = new HttpClient();
            var _RequestSender = new HttpRequestSender(_HttpClient);
            var _Uri = new Uri("https://httpstat.us/200");

            // Act
            var _SearchResults = await _RequestSender.GetStringContentAsync(_Uri);

            // Assert
            Assert.NotNull(_SearchResults);
        } //GetStringContentAsync_ValidRequestUri_ReturnContent

        #endregion //Search Tests

    } //HttpRequestSenderTests
}
