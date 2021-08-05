using ManRack.Repository.Entities;
using ManRack.Repository.Repositories;
using ManRack.Service.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using ManRack.Service.Enums;
using ManRack.Service.Extensions;
using System.Net.Http.Json;

namespace ManRack.Service
{
	public class RateService
	{
		private readonly IRepository<EurExchangeRate> _exchangeRepository;
		private readonly string _exchangeApiUrl = "http://api.exchangeratesapi.io/v1/latest?access_key=a398913480324230b7b9c0563c869454";
		private readonly HttpClient _httpClient;
		private readonly EmailService _emailService;
		private readonly string _japanesseExchangeRateCode = "JPY";

		public RateService(EurExchangeRateRepository exchangeRepository, HttpClient httpClient, EmailService emailService)
		{
			_exchangeRepository = exchangeRepository;
			_httpClient = httpClient;
			_emailService = emailService;
		}

		public async Task CheckForExchangeRate()
		{

			var exchangeResult = await FetchExchangeRate();
			var exchangeSaved = await SaveExchangeRate(exchangeResult);
			var lastExchage = await GetLastExchangeRate();
			ExchangeCompararison comparisonResult = exchangeSaved.CompareExchangeFluctuationTo(lastExchage);
			SendEmailOfExchangeFluctuation(comparisonResult);
		}

		private async Task<ExchangeResult> FetchExchangeRate()
		{

			var exchangeResult = await _httpClient.GetFromJsonAsync<ExchangeResult>(_exchangeApiUrl);

			return exchangeResult;
		}

		private async Task<EurExchangeRate> SaveExchangeRate(ExchangeResult exchangeResult)
		{
			var now = DateTime.Now;

			var eurExchangeRate = new EurExchangeRate()
			{
				JPY = exchangeResult.rates.JPY,
				VerificationTime = now.TimeOfDay,
				VerificationDate = now.Date
			};

			return await _exchangeRepository.Create(eurExchangeRate);
		}

		private async Task<EurExchangeRate> GetLastExchangeRate()
		{
			var exchanges = await _exchangeRepository.GetAll();
			return exchanges.LastOrDefault();
		}

		/// <summary>
		/// Send an email to all subscriptors if fluctuation passed was increase or decrease. Otherwise send nothing
		/// </summary>
		private async void SendEmailOfExchangeFluctuation(ExchangeCompararison comparison)
		{
			//if (comparison.Fluctuation == ExchangeFluctuations.Stable) return;

			string subject = "Notificacion de fluctuación de tasa del Yen Japonés (JPY)";
			string body = $"Hoy dia {DateTime.Now.ToLongDateString()}, el yen valía {comparison.OldValue} y en estos momento está valiendo {comparison.CurrentValue} con respecto al dólar";
			await _emailService.SendEmailToRegisteredSubscriptors(subject, body);
		}

	}
}
