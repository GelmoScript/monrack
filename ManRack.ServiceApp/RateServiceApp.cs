using ManRack.Service;

namespace ManRack.ServiceApp
{
	public class RateServiceApp
	{
		private readonly RateService _rateService;

		public RateServiceApp (RateService rateService)
		{
			_rateService = rateService;
		}
	}
}
