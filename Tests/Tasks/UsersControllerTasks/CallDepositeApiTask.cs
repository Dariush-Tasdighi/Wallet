namespace Tests.Tasks.UsersControllerTasks;

public class CallDepositeApiTask : Base.CallUsersControllerApi
{
	public static CallDepositeApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		var instance =
			new CallDepositeApiTask
			(serverIP: serverIP, databaseContext: databaseContext);

		return instance;
	}

	private CallDepositeApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}

	public Dtat.Result<Dtos.Users.DepositeResponseDto>?
		SendRequest(Dtos.Users.DepositeRequestDto request)
	{
		var deposite =
			Controller.Deposite(request: request);

		var depositeResult =
			deposite.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;


		var depositeValue =
			depositeResult?.Value as
			Dtat.Result<Dtos.Users.DepositeResponseDto>;

		return depositeValue;
	}
}
