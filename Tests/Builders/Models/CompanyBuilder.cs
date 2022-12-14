namespace Tests.Builders.Models;

internal class CompanyBuilder : object
{
	internal static CompanyBuilder Create()
	{
		var newWallet =
			new CompanyBuilder();

		return newWallet;
	}

	private CompanyBuilder() : base()
	{
		IsActive = true;

		Name =
			Helpers.Constants.Shared.Company.Hit;
	}

	protected string? Name { get; private set; }

	protected bool IsActive { get; private set; }

	protected string? Description { get; private set; }

	protected string? AdditionalData { get; private set; }

	internal CompanyBuilder Named(string? name)
	{
		Name = name;

		return this;
	}

	internal CompanyBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	internal CompanyBuilder WithAdditionalData(string? additionalData)
	{
		AdditionalData = additionalData;

		return this;
	}

	internal CompanyBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	internal Domain.Company Build()
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
