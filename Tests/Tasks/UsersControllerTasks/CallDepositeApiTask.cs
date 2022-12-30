using Xunit;

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
		var actionResult =
			Controller.Deposite(request: request);

		Assert.NotNull(@object: actionResult);

		var result =
			actionResult.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.DepositeResponseDto>;

		Assert.NotNull(@object: value);

		return value;
	}
}
