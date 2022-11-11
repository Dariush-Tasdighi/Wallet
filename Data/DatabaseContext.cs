using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public DatabaseContext
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		(Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options) : base(options: options)
	{
		Database.EnsureCreated();
	}

	public Microsoft.EntityFrameworkCore.DbSet<Domain.User> Users { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Company> Companies { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Wallet> Wallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.UserWallet> UserWallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Transaction> Transactions { get; set; }

	protected override void OnConfiguring
		(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly
			(assembly: typeof(Configurations.UserConfiguration).Assembly);
	}

	protected override void ConfigureConventions
		(Microsoft.EntityFrameworkCore.ModelConfigurationBuilder builder)
	{
		//builder.Properties<System.DateOnly>()
		//	.HaveConversion<Conventions.DateTimeConventions.DateOnlyConverter>()
		//	.HaveColumnType(typeName: nameof(System.DateTime.Date))
		//	;

		//builder.Properties<System.DateOnly?>()
		//	.HaveConversion<Conventions.DateTimeConventions.NullableDateOnlyConverter>()
		//	.HaveColumnType(typeName: nameof(System.DateTime.Date))
		//	;
	}
}
