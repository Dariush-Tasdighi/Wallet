namespace Tests.Builders;

internal class GetBalanceRequestUserBuilder : object
{
	#region Create()
	internal static GetBalanceRequestUserBuilder Create()
	{
		return new GetBalanceRequestUserBuilder();
	}
	#endregion /Create()

	#region Constructor
	private GetBalanceRequestUserBuilder() : base()
	{
		IP =
			Helpers.Constants.Shared.Actor.IP;

		CellPhoneNumber =
			Helpers.Constants.Shared.Actor.CellPhoneNumber;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	protected string IP { get; private set; }
	#endregion /IP (User IP)

	#region CellPhoneNumber
	protected string CellPhoneNumber { get; private set; }
	#endregion /CellPhoneNumber

	#endregion /Properties

	#region Methods()
	internal GetBalanceRequestUserBuilder WithIP(string ip)
	{
		IP = ip;

		return this;
	}

	internal GetBalanceRequestUserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.GetBalanceRequestUserDto Build()
	{
		var user =
			new Dtos.Users.GetBalanceRequestUserDto()
			{
				IP = IP,
				CellPhoneNumber = CellPhoneNumber,
			};

		return user;
	}
	#endregion /Build()
}
