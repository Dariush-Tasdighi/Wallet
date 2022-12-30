using Xunit;

namespace Tests.Tasks.UsersControllerTasks;

internal class CallWithdrawApiTask : Base.CallUsersControllerApi
{
	#region Static Member(s)
	internal static CallWithdrawApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		// **************************************************
		var instance =
			new CallWithdrawApiTask
			(serverIP: serverIP, databaseContext: databaseContext);
		// **************************************************

		return instance;
	}
	#endregion /Static Member(s)

	#region Constructor(s)
	private CallWithdrawApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}
	#endregion /Constructor(s)

	#region SendRequest()
	internal Dtat.Result<Dtos.Users.WithdrawResponseDto>?
		SendRequest(Dtos.Users.WithdrawRequestDto request)
	{
		// **************************************************
		var actionResult =
			Controller.Withdraw(request: request);

		Assert.NotNull(@object: actionResult);
		// **************************************************

		// **************************************************
		var result =
			actionResult.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);
		// **************************************************

		// **************************************************
		var value =
			result.Value as
			Dtat.Result<Dtos.Users.WithdrawResponseDto>;

		Assert.NotNull(@object: value);
		// **************************************************

		return value;
	}
	#endregion /SendRequest()
}
