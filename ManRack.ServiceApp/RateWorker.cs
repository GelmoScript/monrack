using ManRack.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonRack.ServiceApp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManRack.ServiceApp
{
	public class RateWorker : BackgroundService
	{
		private readonly ILogger<RateWorker> _logger;
		private readonly RateService _rateService;
		private readonly TrackTimer _trackTimer;
		private readonly LoggerService _loggerService;

		public RateWorker(ILogger<RateWorker> logger, RateService rateService, TrackTimer trackTimer, LoggerService loggerService)
			=> (_logger, _rateService, _trackTimer, _loggerService) = (logger, rateService, trackTimer, loggerService);
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_trackTimer.Interval = _trackTimer.Interval.AddSeconds(10);
			_loggerService.Log($"Service started at {DateTimeOffset.Now}");
			while (!stoppingToken.IsCancellationRequested)
			{
				await _rateService.CheckForExchangeRate();
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				_loggerService.Log($"ExchangeChecked at {DateTimeOffset.Now}");
				await Task.Delay(_trackTimer.Interval.Amount, stoppingToken);
			}
			_loggerService.Log($"Service stopped at {DateTimeOffset.Now}");

		}
	}
}
