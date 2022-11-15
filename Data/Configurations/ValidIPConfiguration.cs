namespace Data.Configurations;

internal class ValidIPConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.ValidIP>
{
	public ValidIPConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.ValidIP> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.ServerIP)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.WalletId, current.ServerIP })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var validIP =
			new Domain.ValidIP
			(walletId: SeedData.Constant.WalletId, serverIP: "::1")
			{
				//Wallet
				//WalletId
				//ServerIP
				//InsertDateTime
				//UpdateDateTime

				RequestCount = 0,
				Description = null,
				LastRequestDateTime = null,
				Id = SeedData.Constant.ValidIP,
			};

		builder.HasData(data: validIP);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
