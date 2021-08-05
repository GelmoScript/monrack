using ManRack.Repository.Contexts;
using ManRack.Repository.Repositories;
using ManRack.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonRack.ServiceApp;

namespace ManRack.ServiceApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseWindowsService(options =>
				{
					options.ServiceName = "ManRackService";
				})
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<RateWorker>();
					services.AddTransient<SubscriptorRepository>();
					services.AddTransient<LoggerService>();
					services.AddTransient<MonRackDbContext>();
					services.AddTransient<TrackTimer>();
					services.AddHttpClient<RateService>();
					services.AddTransient<RateService>();
					services.AddTransient<EmailService>();
					services.AddTransient<MonRackDbContext>();
					services.AddTransient<EurExchangeRateRepository>();
				});
	}
}
