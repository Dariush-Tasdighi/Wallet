namespace Data.Configurations;

internal class InvalidIPConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.InvalidIP>
{
	public InvalidIPConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.InvalidIP> builder)
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
	}
}
