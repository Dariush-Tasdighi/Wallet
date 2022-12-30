using Xunit;

namespace Tests;

public class TestPayment : Helpers.TestsBase
{
	#region Constructor(s)
	public TestPayment
		(Helpers.DatabaseFixture databaseFixture) : base(databaseFixture: databaseFixture)
	{
		Arrange();
	}
	#endregion /Constructor(s)

	#region DoPayment()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 100_000_000, 0)]
	[Xunit.InlineData(500_000_000, 400_000_001, 99_999_999)]
	[Xunit.InlineData(250_000_000, 150_000_000, 100_000_000)]
	public void
		Valid_user_can_successfully_do_payment_while_his_account_has_sufficient_balance_to_pay_the_price
		(decimal depositeAmount, decimal paymentAmount, decimal expectedBalance)
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
		var paymentRequest =
			Builders.PaymentRequestBuilder.Create()
			.WithAmount(amount: paymentAmount)
			.WithWalletToken(walletToken: Wallet.Token)
			.WithCompanyToken(companyToken: Company.Token)
			.WithUser(current => current.WithCellPhoneNumber(cellPhoneNumber: Actor.CellPhoneNumber))
			.Build();

		var paymentValue =
			Tasks.UsersControllerTasks.CallPaymentApiTask
			.Create(serverIP: ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: paymentRequest);

		Assert.NotNull(@object: paymentValue);

		Assert.True(condition: paymentValue.IsSuccess);

		Assert.Equal(expected: 0, actual: paymentValue.ErrorMessages.Count);

		Assert.NotNull(@object: paymentValue.Data);

		Assert.Equal
			(expected: expectedBalance, actual: paymentValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoPayment()

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
			.ThatRefundFeatureIsEnabled()
			.ThatPaymentFeatureIsEnabled()
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
