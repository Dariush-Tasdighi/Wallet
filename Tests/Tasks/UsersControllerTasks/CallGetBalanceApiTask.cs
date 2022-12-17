namespace Tests.Tasks.UsersControllerTasks;

public class CallGetBalanceApiTask : Base.CallUsersControllerApi
{
	public static CallGetBalanceApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		var instance =
			new CallGetBalanceApiTask
			(serverIP: serverIP, databaseContext: databaseContext);

		return instance;
	}

	private CallGetBalanceApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}

	public Dtat.Result<Dtos.Users.GetBalanceResponseDto>?
		SendRequest(Dtos.Users.GetBalanceRequestDto request)
	{
		var getBalance =
			Controller.GetBalance(request: request);

		var getBalanceResult =
			getBalance?.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		var getBalanceValue =
			getBalanceResult?.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		return getBalanceValue;
	}
}
