namespace Data.Configurations;

internal class CompanyConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Company>
{
	public CompanyConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Company> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		//builder
		//	.HasIndex(current => current.Name)
		//	;

		//builder
		//	.HasIndex(current => current.Name)
		//	.IsUnique(unique: true)
		//	;

		builder
			.HasIndex(current => new { current.Name })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.ValidIPs)
			.WithOne(other => other.Company)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CompanyId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.CompanyWallets)
			.WithOne(other => other.Company)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CompanyId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var company =
			new Domain.Company(name: "شرکت داد و ستد هستی")
			{
				//Name
				//Token
				//ValidIPs
				//CompanyWallets
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,
				Description = null,
				AdditionalData = null,
				Id = SeedData.Constant.Id.Company,
			};

		company.UpdateToken
			(token: SeedData.Constant.Token.Company);

		builder.HasData(data: company);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
