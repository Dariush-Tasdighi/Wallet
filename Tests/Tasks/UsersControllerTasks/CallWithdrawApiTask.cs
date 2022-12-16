using Xunit;

namespace Tests.Tasks.UsersControllerTasks;

public class CallWithdrawApiTask : Base.CallUsersControllerApi
{
	public static CallWithdrawApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		var instance =
			new CallWithdrawApiTask
			(serverIP: serverIP, databaseContext: databaseContext);

		return instance;
	}

	private CallWithdrawApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}

	public Dtat.Result<Dtos.Users.WithdrawResponseDto>?
		SendRequest(Dtos.Users.WithdrawRequestDto request)
	{
		var withdraw =
			Controller.Withdraw(request: request);

		var withdrawResult =
			withdraw?.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		var withdrawValue =
			withdrawResult?.Value as
			Dtat.Result<Dtos.Users.WithdrawResponseDto>;

		return withdrawValue;
	}
}
