namespace Data.Configurations;

internal class TransactionConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Transaction>
{
	public TransactionConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Transaction> builder)
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
		builder
			.Property(current => current.UserIP)
			.IsUnicode(unicode: false)
			;
		// **************************************************

		// **************************************************
		builder
			.Property(current => current.ServerIP)
			.IsUnicode(unicode: false)
			;
		// **************************************************

		// **************************************************
		//Precision = Total number of characters used.
		// Scale = Total number after the dot.
		// **************************************************
		builder
			.Property(current => current.Amount)
			.HasPrecision(precision: 18, scale: 2)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.Children)
			.WithOne(other => other.ParentTransaction)
			.IsRequired(required: false)
			.HasForeignKey(other => other.ParentTransactionId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
