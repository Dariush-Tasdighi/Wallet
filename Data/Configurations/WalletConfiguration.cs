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
			.Property(current => current.Name)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.Name })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.Property(current => current.DisplayName)
			.IsUnicode(unicode: true)
			;

		builder
			.HasIndex(current => new { current.DisplayName })
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
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			new Domain.Wallet(companyId: Constant.CompanyId, name: "SEMATEC", displayName: "کیف پول سماتک")
			{
				//Name
				//Token
				//Company
				//CompanyId
				//DisplayName
				//UserWallets
				//Transactions
				//InsertDateTime
				//UpdateDateTime

				ValidIPs = null,
				IsActive = true,
				Id = Constant.WalletId,
				PaymentFeatureIsEnabled = true,
				TransferFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
			};

		builder.HasData(data: wallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
