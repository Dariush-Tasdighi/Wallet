using Xunit;

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
		var payment =
			Controller.Payment(request: request);

		var paymentResult =
			payment?.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		var paymentValue =
			paymentResult?.Value as
			Dtat.Result<Dtos.Users.PaymentResponseDto>;

		return paymentValue;
	}
}

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
		var refund =
			Controller.Refund(request: request);

		var refundResult =
			refund?.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: refundResult);

		var refundValue =
			refundResult?.Value as
			Dtat.Result<Dtos.Users.RefundResponseDto>;

		return refundValue;
	}
}
