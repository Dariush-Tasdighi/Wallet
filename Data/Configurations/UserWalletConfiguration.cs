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
			.HasIndex(current => new { current.UserId, current.WalletId })
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
		//Precision = Total number of characters used.
		// Scale = Total number after the dot.
		// **************************************************
		builder
			.Property(current => current.Balance)
			.HasPrecision(precision: 18, scale: 2)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		//var userWallet =
		//	new Domain.UserWallet
		//	(userId: SeedData.Constant.Id.User,
		//	walletId: SeedData.Constant.Id.Wallet)
		//	{
		//		//Hash

		//		//User
		//		//UserId

		//		//Wallet
		//		//WalletId

		//		//InsertDateTime
		//		//UpdateDateTime

		//		Balance = 0,
		//		IsActive = true,
		//		Description = null,
		//		AdditionalData = null,

		//		PaymentFeatureIsEnabled = true,
		//		WithdrawFeatureIsEnabled = true,
		//		DepositeFeatureIsEnabled = true,

		//		Id = SeedData.Constant.Id.UserWallet,
		//	};

		//builder.HasData(data: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
