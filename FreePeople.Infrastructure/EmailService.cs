using System;
using System.Net;
using System.Net.Mail;
using FreePeople.Domain;
using FreePeople.Domain.Infrastructure;

namespace FreePeople.Infrastructure
{
	public class SmtpConfig
	{
		public string Server { get; set; }
		public string User { get; set; }
		public string Pass { get; set; }
		public int Port { get; set; }
	}

	public class EmailService : IEmailService
	{
		private readonly SmtpConfig _config;

		public EmailService(SmtpConfig config)
		{
			_config = config;
		}

		public void Send(string emailTo, string emailFrom, string title, string text)
		{
			using (var mail = new MailMessage
			{
				From = new MailAddress(emailFrom),
				Subject = title.Replace('\r', ' ').Replace('\n', ' '),
				Body = text,
				IsBodyHtml = true
			})
			{
				mail.To.Add(new MailAddress(emailTo));

				using (var client = new SmtpClient
				{
					Host = _config.Server,
					Port = _config.Port,
					EnableSsl = true,
					DeliveryFormat = SmtpDeliveryFormat.International,
					Credentials = new NetworkCredential(_config.User, _config.Pass)
				})
				{
					client.Send(mail);
					mail.Dispose();
				}
			}
		}
	}
}
