using System;
using System.ComponentModel.DataAnnotations;

namespace FreePeople.Web.Models
{
	public class EditTalkCommand
	{
		[Required]
		public string SpeakerName { get; set; }

		public byte[] SpeakerPhoto { get; set; }
		[Required]
		public string SpeakerAbout { get; set; }
		[Required(ErrorMessage = "Укажите электронную почту")]
		[EmailAddress(ErrorMessage = "Проверьте правильность ввода почты")]
		public string SpeakerEmail { get; set; }
		public string SpeakerFacebook { get; set; }
		[Required]
		[Phone(ErrorMessage = "Нужен правильный телефонный номер в формате +71234567890")]
		public string SpeakerPhone { get; set; }
		[Required]
		public string TalkName { get; set; }
		[Required]
		public string TalkComment { get; set; }
		[Required]
		public string TalkShortInfo { get; set; }
		[Required]
		public string TalkFullInfo { get; set; }
		[Required]
		public DateTime? TalkStartsAt { get; set; }
	}

	public class EditTalkCommandWrapper
	{
		public EditTalkCommand Command { get; set; }
	}

	public class ApproveTalkCommand
	{
		public Guid TalkId { get; set; }
	}

	public class VerifyTalkPlaceCommand
	{
		public Guid TalkId { get; set; }
		public Guid PlaceId { get; set; }
	}

	public class PublishTalkCommand
	{
		public Guid TalkId { get; set; }
	}

	public class ReportTalkCommand
	{
		public Guid TalkId { get; set; }
		public string Report { get; set; }
	}
}