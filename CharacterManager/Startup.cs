using CharacterManager.DAC.Data;
using CharacterManager.Data;
using CharacterManager.Models.Contracts;
using CharacterManager.Worker;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
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

            using (ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
            {
                db.Database.Migrate();
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.AddSingleton<HttpClient>();
            services.AddHostedService<HostedSyncService>();

            services.AddTransient<ICharacterManagerRepository, CharacterManagerRepository>();

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
                BrowserWindowOptions options = new BrowserWindowOptions
                {
                    BackgroundColor = "#121212",
                    Frame = false,
                    Center = true,
                    AutoHideMenuBar = true,
                    Icon = $"{AppDomain.CurrentDomain.BaseDirectory}\\Assets\\icon.png",
                    Title = "Wrath & Glory",
                };

                BrowserWindow window = Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(options)).Result;
            }
        }
    }
}
