namespace Tests.Builders;

internal class DepositeRequestUserBuilder : object
{
	#region Create()
	internal static DepositeRequestUserBuilder Create()
	{
		return new DepositeRequestUserBuilder();
	}
	#endregion /Create()

	#region Constructor
	private DepositeRequestUserBuilder() : base()
	{
		IP =
			Helpers.Constants.Shared.Actor.IP;

		DisplayName =
			Helpers.Constants.Shared.Actor.Reza;

		EmailAddress =
			Helpers.Constants.Shared.Actor.EmailAddress;

		NationalCode =
			Helpers.Constants.Shared.Actor.NationalCode;

		CellPhoneNumber =
			Helpers.Constants.Shared.Actor.CellPhoneNumber;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	protected string IP { get; private set; }
	#endregion /IP (User IP)

	#region DisplayName (Full Name)
	protected string DisplayName { get; private set; }
	#endregion /DisplayName (Full Name)

	#region CellPhoneNumber
	protected string CellPhoneNumber { get; private set; }
	#endregion /CellPhoneNumber

	#region EmailAddress
	protected string? EmailAddress { get; private set; }
	#endregion /EmailAddress

	#region NationalCode
	protected string? NationalCode { get; private set; }
	#endregion /NationalCode

	#endregion /Properties

	#region Methods()
	internal DepositeRequestUserBuilder WithIP(string ip)
	{
		IP = ip;

		return this;
	}

	internal DepositeRequestUserBuilder WithDisplayName(string displayName)
	{
		DisplayName = displayName;

		return this;
	}

	internal DepositeRequestUserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}

	internal DepositeRequestUserBuilder WithNationalCode(string nationalCode)
	{
		NationalCode = nationalCode;

		return this;
	}

	internal DepositeRequestUserBuilder WithEmailAddress(string emailAddress)
	{
		EmailAddress = emailAddress;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.DepositeRequestUserDto Build()
	{
		var user =
			new Dtos.Users.DepositeRequestUserDto()
			{
				IP = IP,
				DisplayName = DisplayName,
				EmailAddress = EmailAddress,
				NationalCode = NationalCode,
				CellPhoneNumber = CellPhoneNumber,
			};

		return user;
	}
	#endregion /Build()
}
