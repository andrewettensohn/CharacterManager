using CharacterManager.DAC.Data;
using CharacterManager.Sync.API.Data;
using CharacterManager.Worker;
using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();

            services.AddSingleton<HttpClient>();
            services.AddTransient<UpSyncRestClient>();
            services.AddTransient<DownSyncRestClient>();
            services.AddHostedService<SyncService>();

            services.AddTransient<ICharacterRepository, CharacterRepository>();

            services.AddDbContextFactory<ApplicationDbContext>(options => {
                options.UseSqlite("Data Source=characterLocal.db").EnableSensitiveDataLogging();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            if (!env.IsDevelopment())
            {
                BrowserWindow window = Task.Run(async () => await Electron.WindowManager.CreateWindowAsync()).Result;
                window.Center();
                window.Maximize();
                window.SetMenuBarVisibility(false);
            }
        }
    }
}
