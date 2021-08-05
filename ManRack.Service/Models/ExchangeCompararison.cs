using ManRack.Service.Enums;

namespace ManRack.Service.Models
{
	public class ExchangeCompararison
	{
		public ExchangeFluctuations Fluctuation { get; set; }
		public double CurrentValue { get; set; }
		public double OldValue { get; set; }
	}
}
