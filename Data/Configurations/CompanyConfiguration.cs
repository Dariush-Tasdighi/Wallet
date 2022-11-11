namespace Data.Configurations;

internal class CompanyConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Company>
{
	public CompanyConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Company> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.Name)
			.IsUnicode(unicode: true)
			;

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
			.HasMany(current => current.Wallets)
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
			new Domain.Company(name: "سماتک")
			{
				//Name
				//Token
				//Wallets
				//InsertDateTime
				//UpdateDateTime

				IsActive = true,
				Description = null,
				Id = Constant.CompanyId,
			};

		builder.HasData(data: company);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
