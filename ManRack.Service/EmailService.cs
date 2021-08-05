using ManRack.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ManRack.Service
{
	public class EmailService
	{
		private readonly string _smtpProvider = "smtp.gmail.com";
		private readonly string _hostEmailAddress = "gerrmosen@gmail.com";
		private readonly string _hostEmailPassword = "Personal@Gmail71";

		private readonly SubscriptorRepository _subscriptorRepository;

		public EmailService(SubscriptorRepository subscriptorRepositor)
		{
			_subscriptorRepository = subscriptorRepositor;
		}
		public async void SendEmail(string subject, string body, params string[] to)
		{
			MailMessage mail = new MailMessage();
			SmtpClient smtpClient = new SmtpClient(_smtpProvider);

			mail.From = new MailAddress(_hostEmailAddress);
			mail.To.Add(string.Join(",", to));
			mail.Subject = subject;
			mail.Body = body;

			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_hostEmailAddress, _hostEmailPassword);
			smtpClient.EnableSsl = true;

			await smtpClient.SendMailAsync(mail);
		}

		public async Task SendEmailToRegisteredSubscriptors(string subject, string body)
		{
			var subscriptors = await _subscriptorRepository.GetAll();
			var emails = subscriptors.Select(subscriptor => subscriptor.Email);
			SendEmail(subject, body, emails.ToArray());
		}
	}
}
