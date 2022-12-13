namespace Tests.Builders;

internal class WithdrawRequestUserBuilder : object
{
	#region Create()
	internal static WithdrawRequestUserBuilder Create()
	{
		return new WithdrawRequestUserBuilder();
	}
	#endregion /Create()

	#region Constructor
	private WithdrawRequestUserBuilder() : base()
	{
		var actor =
			Setups.Users.Reza.Instance;

		IP = actor.IP;
		CellPhoneNumber = actor.User.CellPhoneNumber;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	public string IP { get; set; }
	#endregion /IP (User IP)

	#region CellPhoneNumber
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#endregion /Properties

	#region Methods()
	internal WithdrawRequestUserBuilder WithIP(string ip)
	{
		IP = ip;

		return this;
	}

	internal WithdrawRequestUserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.WithdrawRequestUserDto Build()
	{
		var user =
			new Dtos.Users.WithdrawRequestUserDto()
			{
				IP = IP,
				CellPhoneNumber = CellPhoneNumber,
			};

		return user;
	}
	#endregion /Build()
}
