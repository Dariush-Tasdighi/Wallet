namespace Data.Configurations;

internal class ValidIPConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.ValidIP>
{
	public ValidIPConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.ValidIP> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.ServerIP)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.CompanyId, current.ServerIP })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var validIP1 =
			new Domain.ValidIP
			(companyId: SeedData.Constant.Id.Company, serverIP: "::1")
			{
				//Company
				//ServerIP
				//CompanyId
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,

				Description = null,
				LastRequestDateTime = null,

				TotalRequestCount = 0,
				CurrentDayRequestCount = 0,
				PreviousDay1RequestCount = 0,
				PreviousDay2RequestCount = 0,
				PreviousDay3RequestCount = 0,
				PreviousDay4RequestCount = 0,
				PreviousDay5RequestCount = 0,
				PreviousDay6RequestCount = 0,

				Id = SeedData.Constant.Id.ValidIP1,
			};

		builder.HasData(data: validIP1);
		// **************************************************

		// **************************************************
		var validIP2 =
			new Domain.ValidIP
			(companyId: SeedData.Constant.Id.Company, serverIP: "127.0.0.1")
			{
				//Company
				//ServerIP
				//CompanyId
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,

				Description = null,
				LastRequestDateTime = null,

				TotalRequestCount = 0,
				CurrentDayRequestCount = 0,
				PreviousDay1RequestCount = 0,
				PreviousDay2RequestCount = 0,
				PreviousDay3RequestCount = 0,
				PreviousDay4RequestCount = 0,
				PreviousDay5RequestCount = 0,
				PreviousDay6RequestCount = 0,

				Id = SeedData.Constant.Id.ValidIP2,
			};

		builder.HasData(data: validIP2);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
