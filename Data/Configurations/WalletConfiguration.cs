using Data.Configurations.SeedData;

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
			new Domain.Wallet(companyId: Constant.CompanyId,
			name: "کیف پول سماتک")
			{
				//Name
				//Token

				//Company
				//CompanyId

				//ValidIPs
				//UserWallets
				//Transactions

				//InsertDateTime
				//UpdateDateTime

				Hash = null,
				Description = null,
				AdditionalData = null,

				IsActive = true,
				Id = Constant.WalletId,
				PaymentFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
				//TransferFeatureIsEnabled = true,
			};

		builder.HasData(data: wallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
