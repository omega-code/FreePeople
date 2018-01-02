namespace FreePeople.Domain.Infrastructure
{
	public interface IEmailService
	{
		void Send(string emailTo, string emailFrom, string title, string text);
	}
}