using Microsoft.EntityFrameworkCore;

namespace Tests.Helpers;

public class DatabaseFixture : object, System.IDisposable
{
	public DatabaseFixture() : base()
	{
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: nameof(Server.Controllers.UsersController))
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		DatabaseContext =
			new Data.DatabaseContext(options: options);

		DatabaseContext.Database.EnsureDeleted();
		DatabaseContext.Database.EnsureCreated();
	}

	public Data.DatabaseContext DatabaseContext { get; }

	public void Dispose()
	{
		if (DatabaseContext != null)
		{
			DatabaseContext.Database.EnsureDeleted();

			DatabaseContext.Dispose();
		}
	}

	~DatabaseFixture()
	{
		Dispose();
	}
}
