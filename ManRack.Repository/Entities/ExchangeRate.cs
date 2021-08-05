using System;
using System.Collections.Generic;
using System.Text;

namespace ManRack.Repository.Entities
{
	public class EurExchangeRate
	{
		public int Id { get; set; }
		public TimeSpan? VerificationTime { get; set; }
		public DateTime? VerificationDate { get; set; }
		public double JPY { get; set; }

	}
}
