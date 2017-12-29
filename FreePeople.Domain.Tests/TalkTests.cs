using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optional;
using System;

namespace FreePeople.Domain.Tests
{
    [TestClass]
    public class TalkTests
    {
		private static City DefaultCity = new City(Guid.NewGuid(), "Moscow");
		private static Place DefaultPlace = new Place(Guid.NewGuid(), DefaultCity, "Place", "Address", "", "", "", "");
		private static Speaker DefaultSpeaker = new Speaker(
			Guid.NewGuid(), DefaultCity, 
			"Ivan", new byte[0], "I am", "ivan@gmail.com", 
			Option.None<string>(), Option.None<string>()
		);
		private static Administrator DefaultAdministrator = new Administrator(Guid.NewGuid(), DefaultCity, "Katia", "admin@lvp.com");

		[TestMethod]
        public void WhenCreated_StatusIsNew()
        {
			var talk = new Talk(Guid.NewGuid(), DefaultCity, DateTime.Today.AddDays(1), DefaultSpeaker, "Test", "comment", "", "");
			Assert.AreEqual(TalkStatus.New, talk.Status);
        }

		[TestMethod]
		public void WhenApproved_StatusIsApproved()
		{
			var talk = new Talk(Guid.NewGuid(), DefaultCity, DateTime.Today.AddDays(1), DefaultSpeaker, "Test", "comment", "", "");
			talk.Approve(DateTime.Now, DefaultAdministrator);
			Assert.AreEqual(TalkStatus.Approved, talk.Status);
		}

		[TestMethod]
		public void WhenPlaceVerfified_StatusIsPlaced()
		{
			var talk = new Talk(Guid.NewGuid(), DefaultCity, DateTime.Today.AddDays(1), DefaultSpeaker, "Test", "comment", "", "");
			talk.Approve(DateTime.Now, DefaultAdministrator);
			talk.VerifyPlace(DateTime.Now, DefaultAdministrator, DefaultPlace);
			Assert.AreEqual(TalkStatus.Placed, talk.Status);
		}

		[TestMethod]
		public void WhenPublished_StatusIsPublished()
		{
			var talk = new Talk(Guid.NewGuid(), DefaultCity, DateTime.Today.AddDays(1), DefaultSpeaker, "Test", "comment", "", "");
			talk.Approve(DateTime.Now, DefaultAdministrator);
			talk.VerifyPlace(DateTime.Now, DefaultAdministrator, DefaultPlace);
			talk.Publish(DateTime.Now, DefaultAdministrator);
			Assert.AreEqual(TalkStatus.Published, talk.Status);
		}

		[TestMethod]
		public void WhenReported_StatusIsReported()
		{
			var talk = new Talk(Guid.NewGuid(), DefaultCity, DateTime.Today.AddDays(1), DefaultSpeaker, "Test", "comment", "", "");
			talk.Approve(DateTime.Now, DefaultAdministrator);
			talk.VerifyPlace(DateTime.Now, DefaultAdministrator, DefaultPlace);
			talk.Publish(DateTime.Now, DefaultAdministrator);
			talk.GiveReport(DateTime.Now, DefaultAdministrator, "It was cool");
			Assert.AreEqual(TalkStatus.Reported, talk.Status);
		}
	}
}
