namespace Data.Configurations;

internal class UserConfiguration : object,
	Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.User>
{
	public UserConfiguration() : base()
	{
	}

	public void Configure
		(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.User> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.EmailAddress)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.EmailAddress })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.Property(current => current.NationalCode)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.NationalCode })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.Property(current => current.CellPhoneNumber)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.CellPhoneNumber })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.UserWallets)
			.WithOne(other => other.User)
			.IsRequired(required: true)
			.HasForeignKey(other => other.UserId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Transactions)
			.WithOne(other => other.User)
			.IsRequired(required: true)
			.HasForeignKey(other => other.UserId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.PartyTransactions)
			.WithOne(other => other.PartyUser)
			.IsRequired(required: false) // Note: [false]
			.HasForeignKey(other => other.PartyUserId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
