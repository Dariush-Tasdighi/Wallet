namespace Data.Configurations;

internal class CompanyWalletConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.CompanyWallet>
{
	public CompanyWalletConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.CompanyWallet> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.CompanyId, current.WalletId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var companyWallet =
			new Domain.CompanyWallet
			(companyId: SeedData.Constant.Id.Company,
			walletId: SeedData.Constant.Id.Wallet)
			{
				//Company
				//CompanyId

				//Wallet
				//WalletId

				//InsertDateTime
				//UpdateDateTime

				IsOwner = true,
				IsActive = true,

				Description = null,
				AdditionalData = null,

				Id = SeedData.Constant.Id.CompanyWallet,
			};

		builder.HasData(data: companyWallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
