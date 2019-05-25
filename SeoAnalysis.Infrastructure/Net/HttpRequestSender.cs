using SeoAnalysis.Core.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoAnalysis.Infrastructure.Net
{
    public class HttpRequestSender : IHttpRequestSender
    {

        #region " - - - - - - Fields - - - - - - "

        private readonly HttpClient m_HttpClient;

        #endregion //Fields

        #region " - - - - - - Constructors - - - - - - "

        public HttpRequestSender(HttpClient httpClient)
        {
            this.m_HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #endregion //Constructors

        #region " - - - - - - IHttpRequestSender Implementation - - - - - - "

        /// <summary>
        /// Make a GET Request against a specified URI and return any content as a string.
        /// </summary>
        /// <returns>The response content as a string.</returns>
        /// <exception cref="HttpRequestException">
        /// Will throw an exception if the reponse is not successful.
        /// </exception>
        public async Task<string> GetStringContentAsync(Uri requestUri)
        {
            using (var _RequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (var _ResponseMessage = await this.m_HttpClient.SendAsync(_RequestMessage))
            {
                _ResponseMessage.EnsureSuccessStatusCode();

                return await _ResponseMessage.Content.ReadAsStringAsync();
            } //_RequestMessage, _ResponseMessage

        } //GetStringContentAsync

        #endregion //IHttpRequestSender Implementation

    } //HttpRequestSender
}
