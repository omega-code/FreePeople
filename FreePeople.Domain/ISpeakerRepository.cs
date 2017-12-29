using Optional;

namespace FreePeople.Domain
{
	public interface ISpeakerRepository
	{
		Option<Speaker> FindByEmail(string email);
	}
}