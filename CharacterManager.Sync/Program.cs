using CharacterManager.DAC.Data;
using CharacterManager.DAC.Data.Contracts;
using CharacterManager.DAC.Data.Repositories;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Sync
{
    class Program
    {

        static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddCommandLine(args);
                })
                .ConfigureFunctionsWorker((hostBuilderContext, workerApplicationBuilder) =>
                {
                    workerApplicationBuilder.UseFunctionExecutionMiddleware();
                })
                .ConfigureServices(services =>
                {
                    string connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                    services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

                    services.AddTransient<ITransactionRepository, TransactionRepository>();
                    services.AddTransient<ICharacterRepository, CharacterRepository>();
                    services.AddTransient<IAttributeRepository, AttributeRepository>();
                    services.AddTransient<ISkillsRepository, SkillsRepository>();
                    services.AddTransient<IArchetypeRepository, ArchetypeRepository>();
                    services.AddTransient<IArmorRepository, ArmorRepository>();
                    services.AddTransient<ITalentRepository, TalentRepository>();
                    services.AddTransient<IWeaponRepository, WeaponRepository>();
                    services.AddTransient<IGearRepository, GearRepository>();
                })
                .Build();

            return host.RunAsync();
        }
    }
}
