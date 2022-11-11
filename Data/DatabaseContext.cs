﻿using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
	//#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	//	public DatabaseContext
	//#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	//		(Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options) : base(options: options)
	//	{
	//		// **************************************************
	//		// TODO: Should Delete Later!
	//		// **************************************************
	//		Database.EnsureCreated();
	//		// **************************************************
	//	}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public DatabaseContext() : base()
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

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Company> Companies { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.UserWallet> UserWallets { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Transaction> Transactions { get; set; }

	protected override void OnConfiguring
		(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder: optionsBuilder);

		// **************************************************
		// TODO: Should Delete Later!
		// **************************************************
		var connectionString =
			"Server=.;Database=DTAT_WALLET;MultipleActiveResultSets=true;User ID=sa;Password=1234512345;";

		// UseSqlServer() -> using Microsoft.EntityFrameworkCore;
		optionsBuilder.UseSqlServer
			(connectionString: connectionString);
		// **************************************************
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
