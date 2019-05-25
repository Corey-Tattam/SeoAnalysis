using System;
using System.Threading.Tasks;

namespace SeoAnalysis.Core.Net
{
    public interface IHttpRequestSender
    {
        /// <summary>
        /// Make a GET Request against a specified URI and return any content as a string.
        /// </summary>
        Task<string> GetStringContentAsync(Uri requestUri);

    } //IHttpRequestSender
}
