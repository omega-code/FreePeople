using System;
using System.ComponentModel.DataAnnotations;
using Optional;

namespace FreePeople.Web.Models
{
	public class CreateTalkCommand
	{
		[Required(ErrorMessage = "Обязательно выберите город")]
		public Guid? CityId { get; set; }
		[Required]
		public string SpeakerName { get; set; }
		
		public byte[] SpeakerPhoto { get; set; }
		[Required]
		public string SpeakerAbout { get; set; }
		[Required(ErrorMessage = "Укажите электронную почту")]
		[EmailAddress(ErrorMessage = "Проверьте правильность ввода почты")]
		public string SpeakerEmail { get; set; }
		public string SpeakerFacebook { get; set; }
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

	public class CreateTalkCommandWrapper
	{
		public CreateTalkCommand Command { get; set; }
	}
}
