using System.ComponentModel.DataAnnotations;

namespace FreePeople.Web.Models
{
	public class AuthenticateSpeakerCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
