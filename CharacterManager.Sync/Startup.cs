using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CharacterManager.DAC.Data;
using CharacterManager.DAC.Data.Contracts;
using CharacterManager.DAC.Data.Repositories;

[assembly: FunctionsStartup(typeof(CharacterManager.Sync.Startup))]

namespace CharacterManager.Sync
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

            builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
            builder.Services.AddTransient<IAttributeRepository, AttributeRepository>();
            builder.Services.AddTransient<ISkillsRepository, SkillsRepository>();
            builder.Services.AddTransient<IArchetypeRepository, ArchetypeRepository>();
            builder.Services.AddTransient<IArmorRepository, ArmorRepository>();
            builder.Services.AddTransient<ITalentRepository, TalentRepository>();
            builder.Services.AddTransient<IWeaponRepository, WeaponRepository>();
            builder.Services.AddTransient<IGearRepository, GearRepository>();
        }
    }
}
