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
			.Property(current => current.CompanyUserIdentity)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.WalletId, current.CompanyUserIdentity })
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
			new Domain.UserWallet(userId: Constant.UserId1,
			walletId: Constant.WalletId, companyUserIdentity: "09121087461")
			{
				//Name
				//Token
				//Wallets
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,
				Description = null,
				Id = Constant.UserWalletId1,
			};

		builder.HasData(data: userWallet);
		// **************************************************

		// **************************************************
		userWallet =
			new Domain.UserWallet(userId: Constant.UserId2,
			walletId: Constant.WalletId, companyUserIdentity: "09121087462")
			{
				//Name
				//Token
				//Wallets
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,
				Description = null,
				Id = Constant.UserWalletId2,
			};

		builder.HasData(data: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
