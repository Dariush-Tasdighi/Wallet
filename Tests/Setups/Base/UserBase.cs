namespace Tests.Setups.Base;

internal abstract class UserBase : object
{
	public UserBase() : base()
	{
		IP = string.Empty;

		User =
			new(cellPhoneNumber: string.Empty, displayName: string.Empty);
	}

	public string IP { get; protected set; }

	public Domain.User User { get; protected set; }
}
