using Microsoft.AspNetCore.Mvc;
using SeoAnalysis.Core.Models;
using SeoAnalysis.Core.Services;
using System.Net;
using System.Threading.Tasks;

namespace SeoAnalysis.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class SeoReportsController : BaseController
    {

        #region " - - - - - - Fields - - - - - - "

        private readonly ISeoAnalysisReportService m_SeoReportService;

        #endregion //Fields

        #region " - - - - - - Constructors - - - - - - "

        public SeoReportsController(ISeoAnalysisReportService seoReportService)
        {
            this.m_SeoReportService = seoReportService ?? throw new System.ArgumentNullException(nameof(seoReportService));
        }

        #endregion //Constructors

        #region " - - - - - - Actions - - - - - - "

        [HttpGet]
        [ProducesResponseType(typeof(SeoAnalysisReport), (int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SeoAnalysisReport>> GetSeoAnalysisReport(string keywords, string url) =>
            await this.m_SeoReportService.GetSeoAnalysisReport(keywords, url);

        #endregion //Actions

    } //SeoReportsController
}
