using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManRack.Service
{
	public class LoggerService
	{
		private readonly string _directoryPath;
		public LoggerService(string logFolder = "Logs")
		{
			_directoryPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Logs\\{logFolder}";
		}
		public void Log(string message)
		{
			string filePath = $"{_directoryPath}\\RateService_{DateTime.Now.ToShortDateString().Replace('/', '_')}.txt";
			if (!Directory.Exists(_directoryPath))
			{
				Directory.CreateDirectory(_directoryPath);
			}

			if (!File.Exists(filePath))
			{
				using StreamWriter sw = File.CreateText(filePath);
				sw.WriteLine(message);
			}
			else
			{
				using StreamWriter sw = File.AppendText(filePath);
				sw.WriteLine(message);
			}
		}
	}
}
