using Xunit;

namespace Tests;

public class TestWithdraw : Helpers.TestsBase
{
	#region Constructor(s)
	public TestWithdraw
		(Helpers.DatabaseFixture databaseFixture) : base(databaseFixture: databaseFixture)
	{
	}
	#endregion /Constructor(s)

	#region DoWithdraw()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 99_999_998, 2)]
	[Xunit.InlineData(500_000_000, 490_999_999, 9_000_001)]
	public void Valid_user_can_successfully_do_deposite_after_he_charged_his_wallet
		(decimal depositeAmount, decimal withdrawAmount, decimal expectedBalanceAfterWithdraw)
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

		Assert.True(condition: depositeValue.IsSuccess);
		// **************************************************

		// **************************************************
		var withdrawRequest =
			Builders.WithdrawRequestBuilder.Create()
			.WithAmount(amount: withdrawAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var withdrawValue =
			Tasks.UsersControllerTasks.CallWithdrawApiTask
			.Create(serverIP: companyValidIP.ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: withdrawRequest);

		Assert.NotNull(@object: withdrawValue);

		Assert.True(condition: withdrawValue.IsSuccess);

		Assert.Equal(expected: 0, actual: withdrawValue.ErrorMessages.Count);

		Assert.NotNull(@object: withdrawValue.Data);

		Assert.Equal
			(expected: expectedBalanceAfterWithdraw, actual: withdrawValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoWithdraw()
}
