namespace Data.Configurations;

internal class WalletConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Wallet>
{
	public WalletConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Wallet> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.Name })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.UserWallets)
			.WithOne(other => other.Wallet)
			.IsRequired(required: true)
			.HasForeignKey(other => other.WalletId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Transactions)
			.WithOne(other => other.Wallet)
			.IsRequired(required: true)
			.HasForeignKey(other => other.WalletId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.CompanyWallets)
			.WithOne(other => other.Wallet)
			.IsRequired(required: true)
			.HasForeignKey(other => other.WalletId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			new Domain.Wallet(name: "کیف پول هستی")
			{
				//Name
				//Token

				//UserWallets
				//Transactions
				//CompanyWallets

				//InsertDateTime
				//UpdateDateTime

				Description = null,
				AdditionalData = null,

				IsActive = true,

				RefundFeatureIsEnabled = true,
				PaymentFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
				TransferFeatureIsEnabled = false,

				Id = SeedData.Constant.Id.Wallet,
			};

		wallet.UpdateToken
			(token: SeedData.Constant.Token.Wallet);

		builder.HasData(data: wallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
