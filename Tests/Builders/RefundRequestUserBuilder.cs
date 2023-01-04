namespace Tests.Builders;

internal class RefundRequestUserBuilder : object
{
	#region Create()
	internal static RefundRequestUserBuilder Create()
	{
		return new RefundRequestUserBuilder();
	}
	#endregion /Create()

	#region Constructor
	private RefundRequestUserBuilder() : base()
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
	internal RefundRequestUserBuilder WithIP(string ip)
	{
		IP = ip;

		return this;
	}

	internal RefundRequestUserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.RefundRequestUserDto Build()
	{
		var user =
			new Dtos.Users.RefundRequestUserDto()
			{
				IP = IP,
				CellPhoneNumber = CellPhoneNumber,
			};

		return user;
	}
	#endregion /Build()
}
