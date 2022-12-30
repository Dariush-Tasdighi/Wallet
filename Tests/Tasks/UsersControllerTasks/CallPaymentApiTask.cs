﻿using Xunit;

namespace Tests.Tasks.UsersControllerTasks;

public class CallPaymentApiTask : Base.CallUsersControllerApi
{
	public static CallPaymentApiTask Create
		(string serverIP, Data.DatabaseContext databaseContext)
	{
		var instance =
			new CallPaymentApiTask
			(serverIP: serverIP, databaseContext: databaseContext);

		return instance;
	}

	private CallPaymentApiTask
		(string serverIP, Data.DatabaseContext databaseContext) :
		base(serverIP: serverIP, databaseContext: databaseContext)
	{
	}

	public Dtat.Result<Dtos.Users.PaymentResponseDto>?
		SendRequest(Dtos.Users.PaymentRequestDto request)
	{
		var actionResult =
			Controller.Payment(request: request);

		Assert.NotNull(@object: actionResult);

		var result =
			actionResult.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.PaymentResponseDto>;

		Assert.NotNull(@object: value);

		return value;
	}
}
