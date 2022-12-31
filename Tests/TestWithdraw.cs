using Xunit;

namespace Tests;

public class TestWithdraw : Helpers.TestsBase
{
	#region Constructor(s)
	public TestWithdraw
		(Helpers.DatabaseFixture databaseFixture) : base(databaseFixture: databaseFixture)
	{
		Arrange();
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
		var depositeRequest =
			Builders.DepositeRequestBuilder.Create()
			.WithAmount(amount: depositeAmount)
			.WithWalletToken(walletToken: Wallet.Token)
			.WithCompanyToken(companyToken: Company.Token)
			.WithWithdrawDurationInDays(durationInDays: Helpers.Constants.Shared.WithdrawDurationInDays)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: Actor.CellPhoneNumber))
			.Build();

		var depositeValue =
			Tasks.UsersControllerTasks.CallDepositeApiTask
			.Create(serverIP: ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: depositeRequest);

		Assert.NotNull(@object: depositeValue);

		Assert.True(condition: depositeValue.IsSuccess);
		// **************************************************

		// **************************************************
		var withdrawRequest =
			Builders.WithdrawRequestBuilder.Create()
			.WithAmount(amount: withdrawAmount)
			.WithWalletToken(walletToken: Wallet.Token)
			.WithCompanyToken(companyToken: Company.Token)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: Actor.CellPhoneNumber))
			.Build();

		var withdrawValue =
			Tasks.UsersControllerTasks.CallWithdrawApiTask
			.Create(serverIP: ServerIP, databaseContext: DatabaseContext)
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

	#region Arrange()
	protected override void Arrange()
	{
		// **************************************************
		// **************************************************
		// **************************************************
		Wallet =
			Builders.Models.WalletBuilder.Create()
			.Named(name: Helpers.Constants.Shared.Wallet.Hit)
			.ThatIsActive()
			.ThatWithdrawFeatureIsEnabled()
			.ThatDepositeFeatureIsEnabled()
			.Build();

		var walletToken =
			System.Guid.NewGuid();

		SetupWallet
			(wallet: Wallet, walletToken: walletToken);
		// **************************************************

		// **************************************************
		var companyToken =
			System.Guid.NewGuid();

		Company =
			Builders.Models.CompanyBuilder.Create()
			.Named(name: Helpers.Constants.Shared.Company.Hit)
			.ThatIsActive(isActive: true)
			.Build();

		Company =
			SetupCompany(company: Company, companyToken: companyToken);
		// **************************************************

		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: Company.Id, walletId: Wallet.Id)
		{
			IsActive = true,
		};

		SetupCompanyWallet(companyWallet: companyWallet);
		// **************************************************

		// **************************************************
		ServerIP =
			Helpers.Constants.Shared.Company.ServerIP;

		var validIP = new Domain.ValidIP
			(companyId: Company.Id, serverIP: ServerIP)
		{
			IsActive = true,
		};

		SetupCompanyValidIP(validIP: validIP);
		// **************************************************

		// **************************************************
		Actor =
			Builders.Models.UserBuilder.Create()
			.Named(displayName: Helpers.Constants.Shared.Actor.Reza)
			.WithNationalCode(nationalCode: Helpers.Utility.FakeNationalCode)
			.WithCellPhoneNumber(cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber)
			.ThatIsActive()
			.ThatIsVerified()
			.Build();

		Actor =
			SetupActor(actor: Actor);
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: Actor.Id, walletId: Wallet.Id)
		{
			Balance = 0,
			IsActive = true,
		};

		SetupUserWallet(userWallet: userWallet);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /Arrange()
}
