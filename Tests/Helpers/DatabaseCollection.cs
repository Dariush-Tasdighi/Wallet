namespace Tests.Helpers;

[Xunit.CollectionDefinition
	(name: Setups.Shared.DatabaseCollection)]
public class DatabaseCollection : object,
	Xunit.ICollectionFixture<DatabaseFixture>
{
	public DatabaseCollection() : base()
	{
	}
}
