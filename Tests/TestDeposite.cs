using Microsoft.EntityFrameworkCore;

namespace Tests;

public class TestDeposite : object
{
	#region Constructor(s)
	public TestDeposite() : base()
	{
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: "UsersControllerTest")
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		DatabaseContext =
			new Data.DatabaseContext(options: options);

		DatabaseContext.Database.EnsureDeleted();
		DatabaseContext.Database.EnsureCreated();
	}
	#endregion /Constructor(s)

	#region Property(ies)
	public Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region DoDeposite()
	[Xunit.Fact]
	public void DoDeposite()
	{
		var wallet =
			Constants.Wallets.HastiWallet;

		wallet.UpdateToken(token: Constants.Wallets.HastiWalletToken);
	}
	#endregion /DoDeposite()
}
