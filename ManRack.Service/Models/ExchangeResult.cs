using System;
using System.Collections.Generic;
using System.Text;

namespace ManRack.Service.Models
{
	public class ExchangeResult
	{
		public bool success { get; set; }
		public int timestamp { get; set; }
		public string _base { get; set; }
		public string date { get; set; }
		public Rates rates { get; set; }

	}
	public class Rates
	{
		public float JPY { get; set; }
	}
		
}
