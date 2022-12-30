using Xunit;

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
		var actionResult =
			Controller.GetBalance(request: request);

		Assert.NotNull(@object: actionResult);

		var result =
			actionResult.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: result);

		return value;
	}
}
