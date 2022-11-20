namespace Data.Configurations;

internal class WalletConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Wallet>
{
	public WalletConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Wallet> builder)
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
		builder
			.HasMany(current => current.ValidIPs)
			.WithOne(other => other.Wallet)
			.IsRequired(required: true)
			.HasForeignKey(other => other.WalletId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.InvalidIPs)
			.WithOne(other => other.Wallet)
			.IsRequired(required: true)
			.HasForeignKey(other => other.WalletId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
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
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			new Domain.Wallet(companyId: SeedData.Constant.Id.Company,
			name: "کیف پول هستی")
			{
				//Name
				//Token

				//Company
				//CompanyId

				//ValidIPs
				//InvalidIPs

				//UserWallets
				//Transactions

				//InsertDateTime
				//UpdateDateTime

				Hash = null,
				Description = null,
				AdditionalData = null,

				IsActive = true,

				PaymentFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
				//TransferFeatureIsEnabled = true,

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
