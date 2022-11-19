using Data.Configurations.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class UserWalletConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.UserWallet>
{
	public UserWalletConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.UserWallet> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.WalletId, current.UserId })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.Property(current => current.Hash)
			.IsUnicode(unicode: false)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var userWallet =
			new Domain.UserWallet(userId: Constant.UserId, walletId: Constant.WalletId)
			{
				//Hash

				//User
				//UserId

				//Wallet
				//WalletId

				//InsertDateTime
				//UpdateDateTime

				IsActive = true,
				Description = null,
				AdditionalData = null,

				Id = Constant.UserWalletId,

				PaymentFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,
			};

		builder.HasData(data: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************

		builder
				.Property<int?>("balance")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("Balance")
				.IsConcurrencyToken();
    }
}
