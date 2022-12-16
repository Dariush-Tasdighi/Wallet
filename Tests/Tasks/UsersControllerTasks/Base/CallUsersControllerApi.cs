namespace Tests.Tasks.UsersControllerTasks.Base;

public abstract class CallUsersControllerApi :
	Shared.CallAnApiTask<Server.Controllers.UsersController>
{
	protected CallUsersControllerApi
		(string serverIP, Data.DatabaseContext databaseContext) : base(ip: serverIP)
	{
		Controller =
			new Server.Controllers.UsersController(logger: MockLogger.Object,
			databaseContext: databaseContext, utility: MockUtility.Object);
	}
}
