namespace Tests.Builders.Models;

public class CompanyBuilder : object
{
	public static CompanyBuilder Create()
	{
		var newWallet =
			new CompanyBuilder();

		return newWallet;
	}

	private CompanyBuilder() : base()
	{
		IsActive = true;
		Name = Setups.Constants.Shared.Company.Hit;
	}

	public string Name { get; private set; }

	public bool IsActive { get; private set; }

	public string? Description { get; private set; }

	public string? AdditionalData { get; private set; }

	public CompanyBuilder Named(string name)
	{
		Name = name;

		return this;
	}

	public CompanyBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	public CompanyBuilder WithAdditionalData(string? additionalData)
	{
		AdditionalData = additionalData;

		return this;
	}

	public CompanyBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	public Domain.Company Build()
	{
		var newWallet =
			new Domain.Company(name: Name)
			{
				IsActive = IsActive,
				Description = Description,
				AdditionalData = AdditionalData,
			};

		return newWallet;
	}
}
