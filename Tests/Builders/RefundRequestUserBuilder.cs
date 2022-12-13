namespace Tests.Builders;

internal class RefundRequestUserBuilder : object
{
	#region Create()
	internal static RefundRequestUserBuilder Create()
	{
		return new RefundRequestUserBuilder();
	}
	#endregion /Create()

	#region Properties

	#region IP (User IP)
	public string IP { get; set; }
	#endregion /IP (User IP)

	#region CellPhoneNumber
	public string CellPhoneNumber { get; set; }
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
