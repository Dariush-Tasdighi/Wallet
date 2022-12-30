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

	#region DoWithdraw
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 99_999_998, 2)]
	[Xunit.InlineData(500_000_000, 490_999_999, 9_000_001)]
	public void Valid_user_can_successfully_do_deposite_after_he_charged_his_wallet
		(decimal depositeAmount, decimal withdrawAmount, decimal expectedBalanceAfterWithdraw)
	{
		#region Arrange
		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			Builders.Models.WalletBuilder.Create()
			.Named(name: Helpers.Constants.Shared.Wallet.Hit)
			.ThatIsActive()
			.ThatWithdrawFeatureIsEnabled()
			.ThatDepositeFeatureIsEnabled()
			.Build();

		var walletToken =
			System.Guid.NewGuid();

		SetupWallet
			(wallet: wallet, walletToken: walletToken);
		// **************************************************

		// **************************************************
		var companyToken =
			System.Guid.NewGuid();

		var company =
			Builders.Models.CompanyBuilder.Create()
			.Named(name: Helpers.Constants.Shared.Company.Hit)
			.ThatIsActive(isActive: true)
			.Build();

		company =
			SetupCompany(company: company, companyToken: companyToken);
		// **************************************************

		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: company.Id, walletId: wallet.Id)
		{
			IsActive = true,
		};

		companyWallet =
			SetupCompanyWallet(companyWallet: companyWallet);
		// **************************************************

		// **************************************************
		var serverIP =
			Helpers.Constants.Shared.Company.ServerIP;

		var validIP = new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
		{
			IsActive = true,
		};

		validIP =
			SetupCompanyValidIP(validIP: validIP);
		// **************************************************

		// **************************************************
		var actor =
			Builders.Models.UserBuilder.Create()
			.Named(displayName: Helpers.Constants.Shared.Actor.Reza)
			.WithNationalCode(nationalCode: Helpers.Utility.FakeNationalCode)
			.WithCellPhoneNumber(cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber)
			.ThatIsActive()
			.ThatIsVerified()
			.Build();

		actor =
			SetupActor(actor: actor);
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: actor.Id, walletId: wallet.Id)
		{
			Balance = 0,
			IsActive = true,
		};

		userWallet =
			SetupUserWallet(userWallet: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************
		#endregion /Arrange

		// **************************************************
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
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
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
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
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
	#endregion /DoWithdraw
}
