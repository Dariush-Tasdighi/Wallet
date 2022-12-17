namespace Infrastructure;

public class ControllerBaseWithDatabaseContext : ControllerBase
{
	public ControllerBaseWithDatabaseContext
		(Data.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Data.DatabaseContext DatabaseContext { get; }
}
