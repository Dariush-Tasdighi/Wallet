namespace Tests.Builders.Models;

public class UserBuilder : object
{
	public static UserBuilder Create()
	{
		var newUser =
			new UserBuilder();

		return newUser;
	}

	private UserBuilder() : base()
	{
		IsActive = true;
		IsVerified = true;
		DisplayName = Setups.Constants.Shared.Actor.Reza;
		EmailAddress = Setups.Constants.Shared.Actor.EmailAddress;
		NationalCode = Setups.Constants.Shared.Actor.NationalCode;
		CellPhoneNumber = Setups.Constants.Shared.Actor.CellPhoneNumber;
	}

	public bool IsActive { get; private set; }

	public bool IsVerified { get; private set; }

	public string? NationalCode { get; private set; }

	public string? Description { get; private set; }

	public string? EmailAddress { get; private set; }

	public string DisplayName { get; private set; }

	public string CellPhoneNumber { get; private set; }

	public UserBuilder Named(string displayName)
	{
		DisplayName = displayName;

		return this;
	}

	public UserBuilder WithCellPhoneNumber(string cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}

	public UserBuilder WithEmailAddress(string emailAddress)
	{
		EmailAddress = emailAddress;

		return this;
	}

	public UserBuilder WithNationalCode(string? nationalCode)
	{
		NationalCode = nationalCode;

		return this;
	}

	public UserBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	public UserBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	public UserBuilder ThatIsVerified(bool isVerified = true)
	{
		IsVerified = isVerified;

		return this;
	}

	public Domain.User Build()
	{
		var newUser =
			new Domain.User
			(cellPhoneNumber: CellPhoneNumber, displayName: DisplayName)
			{
				IsActive = IsActive,
				IsVerified = IsVerified,
				Description = Description,
				NationalCode = NationalCode,
				EmailAddress = EmailAddress,
			};

		return newUser;
	}
}
