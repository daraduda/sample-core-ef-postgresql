using DataAccessPostgreSqlProvider;
using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreWebApp
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile("config.json", optional: true, reloadOnChange: true);

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			//Use a PostgreSQL database
			var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

			services.AddDbContext<DomainModelPostgreSqlContext>(options =>
				options.UseNpgsql(
					sqlConnectionString,
					b => b.MigrationsAssembly("CoreWebApp")
				)
			);

			services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider.DataAccessPostgreSqlProvider>();

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();
			loggerFactory.AddDebug();

			app.UseStaticFiles();

			app.UseMvc();
		}
	}
}
