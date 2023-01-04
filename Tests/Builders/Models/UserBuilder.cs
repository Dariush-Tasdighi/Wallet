namespace Tests.Builders.Models;

internal class UserBuilder : object
{
	internal static UserBuilder Create()
	{
		var newUser =
			new UserBuilder();

		return newUser;
	}

	private UserBuilder() : base()
	{
		IsActive = true;

		IsVerified = true;

		DisplayName =
			Helpers.Constants.Shared.Actor.Reza;

		EmailAddress =
			Helpers.Constants.Shared.Actor.EmailAddress;

		NationalCode =
			Helpers.Constants.Shared.Actor.NationalCode;

		CellPhoneNumber =
			Helpers.Constants.Shared.Actor.CellPhoneNumber;
	}

	protected bool IsActive { get; private set; }

	protected bool IsVerified { get; private set; }

	protected string? NationalCode { get; private set; }

	protected string? Description { get; private set; }

	protected string? EmailAddress { get; private set; }

	protected string? DisplayName { get; private set; }

	protected string? CellPhoneNumber { get; private set; }

	internal UserBuilder Named(string? displayName)
	{
		DisplayName = displayName;

		return this;
	}

	internal UserBuilder WithCellPhoneNumber(string? cellPhoneNumber)
	{
		CellPhoneNumber = cellPhoneNumber;

		return this;
	}

	internal UserBuilder WithEmailAddress(string? emailAddress)
	{
		EmailAddress = emailAddress;

		return this;
	}

	internal UserBuilder WithNationalCode(string? nationalCode)
	{
		NationalCode = nationalCode;

		return this;
	}

	internal UserBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	internal UserBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	internal UserBuilder ThatIsVerified(bool isVerified = true)
	{
		IsVerified = isVerified;

		return this;
	}

	internal Domain.User Build()
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
