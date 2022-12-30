﻿namespace Tests.Builders;

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
		IP = Setups.Constants.Shared.Actor.IP;
		DisplayName = Setups.Constants.Shared.Actor.Reza;
		EmailAddress = Setups.Constants.Shared.Actor.EmailAddress;
		NationalCode = Setups.Constants.Shared.Actor.NationalCode;
		CellPhoneNumber = Setups.Constants.Shared.Actor.CellPhoneNumber;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	public string IP { get; set; }
	#endregion /IP (User IP)

	#region DisplayName (Full Name)
	public string DisplayName { get; set; }
	#endregion /DisplayName (Full Name)

	#region CellPhoneNumber
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#region EmailAddress
	public string? EmailAddress { get; set; }
	#endregion /EmailAddress

	#region NationalCode
	public string? NationalCode { get; set; }
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
