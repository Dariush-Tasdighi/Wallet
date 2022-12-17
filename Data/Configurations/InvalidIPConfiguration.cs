namespace Data.Configurations;

internal class InvalidIPConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.InvalidRequestLog>
{
	public InvalidIPConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.InvalidRequestLog> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.ServerIP)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.ServerIP })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
