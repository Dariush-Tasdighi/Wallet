namespace Data.Configurations;

internal class TransactionConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Transaction>
{
	public TransactionConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Transaction> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.Hash)
			.IsUnicode(unicode: false)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
