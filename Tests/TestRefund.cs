using Xunit;

namespace Tests;

public class TestRefund : Helpers.TestsBase
{
	#region Constructor(s)
	public TestRefund
		(Helpers.DatabaseFixture databaseFixture) : base(databaseFixture: databaseFixture)
	{
	}
	#endregion /Constructor(s)

	#region DoRefund()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 99_999_998, 10_999_999, 11_000_001)]
	[Xunit.InlineData(500_000_000, 499_999_999, 499_999_999, 500_000_000)]
	public void Valid_user_can_successfully_do_refund_with_equal_amount_to_his_successful_payment
		(decimal depositeAmount, decimal paymentAmount, decimal refundAmount, decimal expectedBalanceAfterRefund)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			ArrangeWallet(isActive: true);

		var company =
			ArrangeCompany(isActive: true);

		var actor =
			ArrangeActor
			(isActive: true, isVerified: true);

		var companyValidIP =
			ArrangeCompanyValidIP
			(companyId: company.Id, isActive: true);

		var userWallet =
			ArrangeUserWallet
			(walletId: wallet.Id, actorId: actor.Id, isActive: true);

		var companyWallet =
			ArrangeCompanyWallet
			(companyId: company.Id, walletId: wallet.Id, isActive: true);
		// **************************************************

		// **************************************************
		var depositeRequest =
			Builders.DepositeRequestBuilder.Create()
			.WithAmount(amount: depositeAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithWithdrawDurationInDays(durationInDays: Helpers.Constants.Shared.WithdrawDurationInDays)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var depositeValue =
			Tasks.UsersControllerTasks.CallDepositeApiTask
			.Create(serverIP: companyValidIP.ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: depositeRequest);

		Assert.NotNull(@object: depositeValue);

		Assert.NotNull(@object: depositeValue.Data);

		Assert.True(condition: depositeValue.IsSuccess);
		// **************************************************

		// **************************************************
		var paymentRequest =
			Builders.PaymentRequestBuilder.Create()
			.WithAmount(amount: paymentAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var paymentValue =
			Tasks.UsersControllerTasks.CallPaymentApiTask
			.Create(serverIP: companyValidIP.ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: paymentRequest);

		Assert.NotNull(@object: paymentValue);

		Assert.NotNull(@object: paymentValue.Data);

		Assert.True(condition: paymentValue.IsSuccess);
		// **************************************************

		// **************************************************
		var refundRequest =
			Builders.RefundRequestBuilder
			.Create(transactionId: paymentValue.Data.TransactionId)
			.WithAmount(amount: refundAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var refundValue =
			Tasks.UsersControllerTasks.CallRefundApiTask
			.Create(serverIP: companyValidIP.ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: refundRequest);

		Assert.NotNull(@object: refundValue);

		Assert.True(condition: refundValue.IsSuccess);

		Assert.Equal(expected: 0, actual: refundValue.ErrorMessages.Count);

		Assert.NotNull(@object: refundValue.Data);

		Assert.Equal
			(expected: expectedBalanceAfterRefund, actual: refundValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoRefund()
}
