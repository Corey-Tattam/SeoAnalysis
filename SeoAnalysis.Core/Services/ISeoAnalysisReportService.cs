using SeoAnalysis.Core.Models;
using System.Threading.Tasks;

namespace SeoAnalysis.Core.Services
{
    public interface ISeoAnalysisReportService
    {

        /// <summary>
        /// Given a keywords string and URL, retrieve an S.E.O. Report. 
        /// </summary>
        /// <param name="keywords">The keywords to search.</param>
        /// <param name="url">The URL to match.</param>
        /// <returns>An SeoAnalysisReport.</returns>
        Task<SeoAnalysisReport> GetSeoAnalysisReport(string keywords, string url);

    } //ISeoAnalysisReportService
}
