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
		#region Arrange
		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			Builders.Models.WalletBuilder.Create()
			.Named(name: Setups.Constants.Shared.Wallet.Hit)
			.ThatIsActive()
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
			SetupCompany
			(name: Setups.Constants.Shared.Company.Hit,
			companyToken: companyToken, isActive: true);
		// **************************************************

		// **************************************************
		var companyWallet =
			SetupCompanyWallet
			(companyId: company.Id, walletId: wallet.Id, isActive: true);
		// **************************************************

		// **************************************************
		var serverIP =
			Setups.Constants.Shared.Company.ServerIP;

		var validIP =
			SetupCompanyValidIP
			(companyId: company.Id, serverIP: serverIP, isActive: true);
		// **************************************************

		// **************************************************
		var actor =
			SetupActor
			(isActive: true, isVerified: true,
			displayName: Setups.Constants.Shared.Actor.Reza,
			nationalCode: Helpers.Utility.FakeNationalCode,
			emailAddress: Helpers.Utility.FakeEmailAddress,
			cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber);
		// **************************************************

		// **************************************************
		var userWallet =
			SetupUserWallet
			(userId: actor.Id, walletId: wallet.Id);
		// **************************************************
		// **************************************************
		// **************************************************
		#endregion /Arrange

		// **************************************************
		// **************************************************
		// **************************************************
		var getBalanceRequest =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = wallet.Token,
				CompanyToken = company.Token,
			};

		getBalanceRequest.User.CellPhoneNumber = actor.CellPhoneNumber;

		var getBalanceValue =
			Tasks.UsersControllerTasks.CallGetBalanceApiTask
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
			.SendRequest(request: getBalanceRequest);

		Assert.NotNull(@object: getBalanceValue);

		Assert.True(condition: getBalanceValue.IsSuccess);
		// **************************************************

		// **************************************************
		var depositeRequest =
			Builders.DepositeRequestBuilder.Create()
			.WithAmount(amount: depositeAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithWithdrawDurationInDays(durationInDays: Setups.Constants.Shared.WithdrawDurationInDays)
			.WithUser(current => current
				.WithIP(ip: Setups.Constants.Shared.Actor.IP)
				.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var depositeValue =
			Tasks.UsersControllerTasks.CallDepositeApiTask
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
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
