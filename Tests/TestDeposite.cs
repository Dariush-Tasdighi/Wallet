using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class TestDeposite : object
{
	#region Constructor(s)
	public TestDeposite() : base()
	{
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: "UsersControllerTest-DepositeScenario")
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		DatabaseContext =
			new Data.DatabaseContext(options: options);

		DatabaseContext.Database.EnsureDeleted();
		DatabaseContext.Database.EnsureCreated();
	}
	#endregion /Constructor(s)

	#region Property(ies)
	public Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region DoDeposite()
	[Xunit.Fact]
	public void DoDeposite()
	{
		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			Constants.Wallets.HastiWallet;

		wallet.DepositeFeatureIsEnabled = true;

		wallet.UpdateToken
			(token: Constants.Wallets.HastiWalletToken);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************
		var company =
			Constants.Companies.Hit;

		company.UpdateToken
			(token: Constants.Companies.HitCompanyToken);

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
			(companyId: company.Id, serverIP: Constants.Companies.HitIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var user = Constants.Users.Reza;

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
			.Returns(value: Constants.Companies.HitIP);
		// **************************************************

		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		// **************************************************
		// **************************************************
		// **************************************************
		// **************************************************
		// **************************************************
		var getBalanceRequest =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = Constants.Wallets.HastiWalletToken,
				CompanyToken = Constants.Companies.HitCompanyToken,
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
		// **************************************************

		// **************************************************
		var depositeRequest =
			Builders.DepositeRequestBuilder.Create()
			.WithWalletToken(walletToken: Constants.Wallets.HastiWalletToken)
			.WithCompanyToken(companyToken: Constants.Companies.HitCompanyToken)
			.WithAmount(amount: 100_000_000)
			.WithWithdrawDurationInDays(withdrawDurationInDays: 0)
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
			(expected: depositeRequest.Amount, actual: depositeValue.Data.Balance);
		// **************************************************
	}
	#endregion /DoDeposite()
}
