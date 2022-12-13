namespace Tests.Builders;

internal class PaymentRequestUserBuilder : object
{
	#region Create()
	internal static PaymentRequestUserBuilder Create()
	{
		return new PaymentRequestUserBuilder();
	}
	#endregion /Create()

	#region Constructor
	private PaymentRequestUserBuilder() : base()
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
	internal PaymentRequestUserBuilder WithIP(string ip)
	{
		IP = ip;

		return this;
	}

	internal PaymentRequestUserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.PaymentRequestUserDto Build()
	{
		var user =
			new Dtos.Users.PaymentRequestUserDto()
			{
				IP = IP,
				CellPhoneNumber = CellPhoneNumber,
			};

		return user;
	}
	#endregion /Build()
}
