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
		var hitWallet =
			Setups.Wallet.Hit.Instance;

		var wallet = hitWallet.Wallet;

		wallet.RefundFeatureIsEnabled = true;
		wallet.PaymentFeatureIsEnabled = true;
		wallet.DepositeFeatureIsEnabled = true;

		wallet.UpdateToken
			(token: hitWallet.Token);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var hitCompany =
			Setups.Company.Hit.Instance;

		var company = hitCompany.Company;

		company.UpdateToken
			(token: hitCompany.Token);

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
			(companyId: company.Id, serverIP: hitCompany.IP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var actor =
			Setups.Users.Reza.Instance;

		var user = actor.User;

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: user.Id, walletId: wallet.Id)
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
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: hitCompany.IP);
		// **************************************************

		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		// **************************************************
		// **************************************************
		// **************************************************
		var getBalanceRequest =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = hitWallet.Token,
				CompanyToken = hitCompany.Token,
			};

		getBalanceRequest.User.CellPhoneNumber = user.CellPhoneNumber;

		var getBalance =
			usersController.GetBalance(request: getBalanceRequest);

		Assert.NotNull(@object: getBalance);

		var getBalanceResult =
			getBalance.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: getBalanceResult);

		var getBalanceValue =
			getBalanceResult.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

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
			.WithWalletToken(walletToken: hitWallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Token)
			.WithWithdrawDurationInDays(withdrawDurationInDays: Setups.Constants.Shared.WithdrawDurationInDaysNeutralValue)
			.Build();

		depositeRequest.User.CellPhoneNumber = user.CellPhoneNumber;

		var deposite =
			usersController.Deposite(request: depositeRequest);

		Assert.NotNull(@object: deposite);

		var depositeResult =
			deposite.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: depositeResult);

		var depositeValue =
			depositeResult.Value as
			Dtat.Result<Dtos.Users.DepositeResponseDto>;

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
			.WithWalletToken(walletToken: hitWallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Token)
			.WithAmount(amount: paymentAmount)
			.Build();

		paymentRequest.User.IP = Setups.Constants.Shared.UserIP;
		paymentRequest.User.CellPhoneNumber = user.CellPhoneNumber;

		var payment =
			usersController.Payment(request: paymentRequest);

		Assert.NotNull(@object: payment);

		var paymentResult =
			payment.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: paymentResult);

		var paymentValue =
			paymentResult.Value as
			Dtat.Result<Dtos.Users.PaymentResponseDto>;

		Assert.NotNull(@object: paymentValue);

		Assert.True(condition: paymentValue.IsSuccess);

		Assert.Equal(expected: 0, actual: paymentValue.ErrorMessages.Count);

		Assert.NotNull(@object: paymentValue.Data);

		Assert.Equal
			(expected: expectedBalanceAfterPayment, actual: paymentValue.Data.Balance);
		// **************************************************

		// **************************************************
		var refundRequest =
			Builders.RefundRequestBuilder.Create(transactionId: paymentValue.Data.TransactionId)
			.WithWalletToken(walletToken: hitWallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Token)
			.WithAmount(amount: refundAmount)
			.Build();

		refundRequest.User.IP = Setups.Constants.Shared.UserIP;
		refundRequest.User.CellPhoneNumber = user.CellPhoneNumber;

		var refund =
			usersController.Refund(request: refundRequest);

		Assert.NotNull(@object: payment);

		var refundResult =
			refund.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: refundResult);

		var refundValue =
			refundResult.Value as
			Dtat.Result<Dtos.Users.RefundResponseDto>;

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
