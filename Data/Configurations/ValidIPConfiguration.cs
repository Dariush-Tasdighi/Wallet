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

				IsActive = true,

				Description = null,
				LastRequestDateTime = null,
				Id = SeedData.Constant.ValidIP,

				TotalRequestCount = 0,
				CurrentDayRequestCount = 0,
				PreviousDay1RequestCount = 0,
				PreviousDay2RequestCount = 0,
				PreviousDay3RequestCount = 0,
				PreviousDay4RequestCount = 0,
				PreviousDay5RequestCount = 0,
				PreviousDay6RequestCount = 0,
			};

		builder.HasData(data: validIP);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
