using Xunit;

namespace Tests.Tasks.UsersControllerTasks;

public class CallRefundApiTask : Base.CallUsersControllerApi
{
	public static CallRefundApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		var instance =
			new CallRefundApiTask
			(serverIP: serverIP, databaseContext: databaseContext);

		return instance;
	}

	private CallRefundApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}

	public Dtat.Result<Dtos.Users.RefundResponseDto>?
		SendRequest(Dtos.Users.RefundRequestDto request)
	{
		var actionResult =
			Controller.Refund(request: request);

		Assert.NotNull(@object: actionResult);

		var result =
			actionResult.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.RefundResponseDto>;

		Assert.NotNull(@object: value);

		return value;
	}
}
