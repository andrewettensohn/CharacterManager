using CharacterManager.DAC.Data;
using CharacterManager.DAC.Data.Contracts;
using CharacterManager.DAC.Data.Repositories;
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
using System.Threading.Tasks;

namespace CharacterManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //using (ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), configuration))
            //{
            //    db.Database.Migrate();
            //}
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();

            services.AddTransient<ITransactionRepository, TransactionRepository>();

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IAttributeRepository, AttributeRepository>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();
            services.AddTransient<IArchetypeRepository, ArchetypeRepository>();
            services.AddTransient<IArmorRepository, ArmorRepository>();
            services.AddTransient<ITalentRepository, TalentRepository>();
            services.AddTransient<IWeaponRepository, WeaponRepository>();
            services.AddTransient<IGearRepository, GearRepository>();

            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            if(!env.IsDevelopment())
            {
                BrowserWindow window = Task.Run(async () => await Electron.WindowManager.CreateWindowAsync()).Result;
                window.Center();
                window.Maximize();
                window.SetMenuBarVisibility(false);
            }
        }
    }
}
