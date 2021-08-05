using ManRack.Repository.Entities;
using ManRack.Service.Enums;
using ManRack.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManRack.Service.Extensions
{
	public static class ExchangeRateExtension
	{
		public static ExchangeCompararison CompareExchangeFluctuationTo(this EurExchangeRate exchageToCompare, EurExchangeRate exchangeReference)
		{
			var exchangeComparison = new ExchangeCompararison()
			{
				OldValue = exchageToCompare.JPY,
				CurrentValue = exchangeReference.JPY
			};

			if (exchangeReference.JPY == exchageToCompare.JPY)
			{
				exchangeComparison.Fluctuation = ExchangeFluctuations.Stable;
				return exchangeComparison;
			}
			
			exchangeComparison.Fluctuation = exchangeReference.JPY > exchageToCompare.JPY
				? ExchangeFluctuations.Decrease
				: ExchangeFluctuations.Increase;

			return exchangeComparison;
		}
	}
}
