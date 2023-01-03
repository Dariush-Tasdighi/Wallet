using Xunit;

namespace Tests;

public class TestDeposite : Helpers.TestsBase
{
	#region Constructor(s)
	public TestDeposite
		(Helpers.DatabaseFixture databaseFixture) : base(databaseFixture: databaseFixture)
	{
	}
	#endregion /Constructor(s)

	#region DoDeposite()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 100_000_000)]
	[Xunit.InlineData(500_000_000, 500_000_000)]
	[Xunit.InlineData(250_000_000, 250_000_000)]
	public void
		Valid_user_can_successfully_do_deposite_with_valid_information
		(decimal depositeAmount, decimal expectedBalance)
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

		Assert.Equal(expected: 0, actual: depositeValue.ErrorMessages.Count);

		Assert.NotNull(@object: depositeValue.Data);

		Assert.Equal
			(expected: expectedBalance, actual: depositeValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoDeposite()
}
