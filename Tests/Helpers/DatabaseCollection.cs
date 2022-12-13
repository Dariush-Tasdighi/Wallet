namespace Tests.Helpers;

[Xunit.CollectionDefinition
	(name: Setups.Constants.Shared.DatabaseCollection)]
public class DatabaseCollection : object,
	Xunit.ICollectionFixture<DatabaseFixture>
{
	public DatabaseCollection() : base()
	{
	}
}
