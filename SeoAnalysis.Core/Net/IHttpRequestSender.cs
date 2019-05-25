using System;
using System.Threading.Tasks;

namespace SeoAnalysis.Core.Net
{
    public interface IHttpRequestSender
    {

        Task<string> GetStringContentAsync(Uri requestUri);

    } //IHttpRequestSender
}
