using Xunit;

namespace Tests;

[Xunit.Collection
	(name: Setups.Constants.Shared.DatabaseCollection)]
public class TestDeposite : object
{
	#region Constructor(s)
	public TestDeposite(Helpers.DatabaseFixture databaseFixture) : base()
	{
		DatabaseContext =
			databaseFixture.DatabaseContext;
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region DoDeposite()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 100_000_000)]
	[Xunit.InlineData(500_000_000, 500_000_000)]
	[Xunit.InlineData(250_000_000, 250_000_000)]
	public void DoDeposite(decimal depositeAmount, decimal expectedBalance)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		var hitWallet =
			Setups.Wallet.Hit.Instance;

		var wallet = hitWallet.Wallet;

		wallet.DepositeFeatureIsEnabled = true;

		wallet.UpdateToken(token: hitWallet.Token);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var hitCompany =
			Setups.Company.Hit.Instance;

		var company = hitCompany.Company;

		company.UpdateToken(token: hitCompany.Token);

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
			.WithWalletToken(walletToken: wallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Company.Token)
			.WithWithdrawDurationInDays(withdrawDurationInDays: Setups.Constants.Shared.WithdrawDurationInDaysNeutralValue)
			.Build();

		depositeRequest.User.CellPhoneNumber = user.CellPhoneNumber;

		var deposite =
			usersController.Deposite(request: depositeRequest);

		Assert.NotNull(@object: getBalance);

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
			(expected: expectedBalance, actual: depositeValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoDeposite()
}
