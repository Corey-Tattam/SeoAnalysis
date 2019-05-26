using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeoAnalysis.Core.Net;
using SeoAnalysis.Core.SearchEngines;
using SeoAnalysis.Core.Services;
using SeoAnalysis.Infrastructure.Net;
using SeoAnalysis.Infrastructure.SearchEngines;
using SeoAnalysis.Infrastructure.Services;

namespace SeoAnalysis.WebUI
{
    public class Startup
    {

        #region " - - - - - - Constructors - - - - - - "

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion //Constructors

        #region " - - - - - - Properties - - - - - - "

        public IConfiguration Configuration { get; }

        #endregion //Properties

        #region " - - - - - - Methods - - - - - - "

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add HttpClient support.
            services.AddHttpClient("SeoAnalysis")
                    .AddTypedClient<IHttpRequestSender, HttpRequestSender>();

            // Add Search Engine Functionality.
            services.AddTransient<ISearchEngineResultsPageAnalyser,                             GoogleResultsPageAnalyser>()
                    .AddTransient<ISearchEngineResultsPageParser<GoogleResultsPageAnalyser>,    GoogleResultsPageParser>()
                    .AddTransient<ISearchUrlProvider<GoogleResultsPageAnalyser>,                GoogleSearchUrlProvider>()
            ;

            // Add Services
            services.AddTransient<ISeoAnalysisReportService, SeoAnalysisReportService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        } //ConfigureServices

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        } //Configure

        #endregion //Methods

    } //Startup
}
