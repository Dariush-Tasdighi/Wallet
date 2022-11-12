namespace Infrastructure;

public class ControllerBaseWithDatabaseContext : ControllerBase
{
	public ControllerBaseWithDatabaseContext
		(Data.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Data.DatabaseContext DatabaseContext { get; }

	//protected async
	//	System.Threading.Tasks.Task DisposeDatabaseContextAsync()
	//{
	//	if (DatabaseContext != null)
	//	{
	//		await DatabaseContext.DisposeAsync();

	//		//DatabaseContext = null;
	//	}
	//}
}
