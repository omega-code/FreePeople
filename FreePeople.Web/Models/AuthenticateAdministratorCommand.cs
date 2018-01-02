using System.ComponentModel.DataAnnotations;

namespace FreePeople.Web.Models
{
	public class AuthenticateAdministratorCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string PIN { get; set; }

	}
}
