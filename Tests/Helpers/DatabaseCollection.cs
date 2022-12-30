namespace Tests.Helpers;

[Xunit.CollectionDefinition
	(name: Constants.Shared.DatabaseCollection)]
public class DatabaseCollection : object,
	Xunit.ICollectionFixture<DatabaseFixture>
{
	public DatabaseCollection() : base()
	{
	}
}
