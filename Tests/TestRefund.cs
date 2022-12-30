using Xunit;

namespace Tests;

[Xunit.Collection
	(name: Setups.Constants.Shared.DatabaseCollection)]
public class TestRefund : object
{
	#region Constructor(s)
	public TestRefund(Helpers.DatabaseFixture databaseFixture) : base()
	{
		DatabaseContext =
			databaseFixture.DatabaseContext;
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region DoRefund
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 100_000_000, 99_999_998, 2, 10_999_999, 11_000_001)]
	[Xunit.InlineData(500_000_000, 500_000_000, 499_999_999, 1, 499_999_999, 500_000_000)]
	public void DoRefund
		(decimal depositeAmount, decimal expectedBalanceAfterDeposite,
		decimal paymentAmount, decimal expectedBalanceAfterPayment,
		decimal refundAmount, decimal expectedBalanceAfterRefund)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			Builders.Models.WalletBuilder.Create()
			.Named(name: Setups.Constants.Shared.Wallet.Hit)
			.ThatIsActive()
			.ThatRefundFeatureIsEnabled()
			.ThatPaymentFeatureIsEnabled()
			.ThatDepositeFeatureIsEnabled()
			.Build();

		var walletToken =
			System.Guid.NewGuid();

		wallet.UpdateToken
			(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var serverIP =
			Setups.Constants.Shared.Company.ServerIP;

		var company =
			Builders.Models.CompanyBuilder.Create()
			.Named(name: Setups.Constants.Shared.Company.Hit)
			.ThatIsActive()
			.Build();

		var companyToken =
			System.Guid.NewGuid();

		company.UpdateToken
			(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: company.Id, walletId: wallet.Id)
		{
			IsActive = true,
		};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var actor =
			Builders.Models.UserBuilder.Create()
			.Named(displayName: Setups.Constants.Shared.Actor.Reza)
			.WithNationalCode(nationalCode: Helpers.Utility.FakeNationalCode)
			.WithEmailAddress(emailAddress: Helpers.Utility.FakeEmailAddress)
			.WithCellPhoneNumber(cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber)
			.ThatIsActive()
			.ThatIsVerified()
		.Build();

		actor.UpdateHash();

		DatabaseContext.Add(entity: actor);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: actor.Id, walletId: wallet.Id)
		{
			Balance = 0,
			IsActive = true,
		};

		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************
		// **************************************************
		// **************************************************

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

		Assert.Equal(expected: 0, actual: getBalanceValue.ErrorMessages.Count);

		Assert.NotNull(@object: getBalanceValue.Data);

		Assert.Equal
			(expected: 0, actual: getBalanceValue.Data.Balance);
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
			(expected: expectedBalanceAfterDeposite, actual: depositeValue.Data.Balance);
		// **************************************************

		// **************************************************
		var paymentRequest =
			Builders.PaymentRequestBuilder.Create()
			.WithAmount(amount: paymentAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithUser(current => current
				.WithIP(ip: Setups.Constants.Shared.Actor.IP)
				.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var paymentValue =
			Tasks.UsersControllerTasks.CallPaymentApiTask
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
			.SendRequest(request: paymentRequest);

		Assert.NotNull(@object: paymentValue);

		Assert.True(condition: paymentValue.IsSuccess);

		Assert.Equal(expected: 0, actual: paymentValue.ErrorMessages.Count);

		Assert.NotNull(@object: paymentValue.Data);

		Assert.Equal
			(expected: expectedBalanceAfterPayment, actual: paymentValue.Data.Balance);
		// **************************************************

		// **************************************************
		var refundRequest =
			Builders.RefundRequestBuilder
			.Create(transactionId: paymentValue.Data.TransactionId)
			.WithAmount(amount: refundAmount)
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: company.Token)
			.WithUser(current => current
				.WithIP(ip: Setups.Constants.Shared.Actor.IP)
				.WithCellPhoneNumber(cellPhoneNumber: actor.CellPhoneNumber))
			.Build();

		var refundValue =
			Tasks.UsersControllerTasks.CallRefundApiTask
			.Create(serverIP: serverIP, databaseContext: DatabaseContext)
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
	#endregion /DoRefund
}
