using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

/// <summary>
/// TODOS
/// 
/// Servicio de envio de email
/// Llamada de api
/// Logica de comparacion
/// Logica de Base de datos
/// 
/// </summary>

namespace MonRack.ServiceApp
{
	public class TrackTimer
	{
		public TimerInterval Interval { get; set; }
		public readonly Timer _timer;

		public TrackTimer()
		{
			_timer = new Timer();
			Interval = new TimerInterval(0);
		}

		public void AddEventHandler(Action<object, ElapsedEventArgs> action) 
		{
			_timer.Elapsed += new ElapsedEventHandler(action);
		}
	}

	public struct TimerInterval
	{
		private int _amount;

		public int Amount { get => _amount; }

		public TimerInterval(int amount)
		{
			_amount = amount;
		}

		public TimerInterval AddSeconds(int amount)
		{
			int ms = 1000;
			int amountToAdd = ms * amount;
			_amount += Math.Abs(amountToAdd);
			return this;
		}

		public TimerInterval AddMinutes(int amount)
		{
			return AddSeconds(60 * amount);
		}

		public TimerInterval AddHours(int amount)
		{
			return AddMinutes(60 * amount);
		}
	}
}
