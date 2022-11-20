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
			new Domain.UserWallet
			(userId: SeedData.Constant.Id.User,
			walletId: SeedData.Constant.Id.Wallet)
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

				Balance = 0,

				PaymentFeatureIsEnabled = true,
				WithdrawFeatureIsEnabled = true,
				DepositeFeatureIsEnabled = true,

				Id = SeedData.Constant.Id.UserWallet,
			};

		builder.HasData(data: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
