using CharacterManager.Data;
using CharacterManager.Data.Contracts;
using CharacterManager.Data.Repositories;
using CharacterManager.Services;
using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<ICharacterRepository, CharacterRepository>();
            services.AddSingleton<IAttributeRepository, AttributeRepository>();
            services.AddSingleton<ISkillsRepository, SkillsRepository>();
            services.AddSingleton<IArchetypeRepository, ArchetypeRepository>();
            services.AddSingleton<IArmorRepository, ArmorRepository>();
            services.AddSingleton<ITalentRepository, TalentRepository>();
            services.AddSingleton<IWeaponRepository, WeaponRepository>();
            services.AddSingleton<IGearRepository, GearRepository>();

            services.AddSingleton<CharacterService>();
            services.AddSingleton<TalentService>();
            services.AddSingleton<ArchetypeService>();
            services.AddSingleton<ArmorService>();
            services.AddSingleton<WeaponService>();
            services.AddSingleton<GearService>();

            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
