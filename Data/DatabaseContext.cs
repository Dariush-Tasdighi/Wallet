namespace Data;

public class DatabaseContext :
	Microsoft.EntityFrameworkCore.DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public DatabaseContext
		(Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options) : base(options: options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		// **************************************************
		// TODO: Should Delete Later!
		// **************************************************
		Database.EnsureCreated();
		// **************************************************
	}

	public Microsoft.EntityFrameworkCore.DbSet<Domain.User> Users { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Wallet> Wallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.ValidIP> ValidIPs { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Company> Companies { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.UserWallet> UserWallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Transaction> Transactions { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.CompanyWallet> CompanyWallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.InvalidRequestLog> InvalidRequestLogs { get; set; }

	protected override void OnConfiguring
		(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder: optionsBuilder);
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
	}
}
