using Xunit;

namespace Tests;

[Xunit.Collection
	(name: Setups.Constants.Shared.DatabaseCollection)]
public class TestPayment : object
{
	#region Constructor(s)
	public TestPayment(Helpers.DatabaseFixture databaseFixture) : base()
	{
		DatabaseContext =
			databaseFixture.DatabaseContext;
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region DoPayment()
	[Xunit.Theory]
	[Xunit.InlineData(100_000_000, 100_000_000, 0)]
	[Xunit.InlineData(500_000_000, 400_000_001, 99_999_999)]
	[Xunit.InlineData(250_000_000, 150_000_000, 100_000_000)]
	public void DoPayment(decimal depositeAmount, decimal paymentAmount, decimal expectedBalance)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		var hitWallet =
			Setups.Wallet.Hit.Instance;

		hitWallet.Wallet.PaymentFeatureIsEnabled = true;
		hitWallet.Wallet.DepositeFeatureIsEnabled = true;

		hitWallet.Wallet.UpdateToken(token: hitWallet.Token);

		DatabaseContext.Add(entity: hitWallet.Wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var hitCompany =
			Setups.Company.Hit.Instance;

		hitCompany.Company.UpdateToken
			(token: hitCompany.Token);

		DatabaseContext.Add(entity: hitCompany.Company);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: hitCompany.Company.Id, walletId: hitWallet.Wallet.Id)
		{
			IsActive = true,
		};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var validIP =
			new Domain.ValidIP
			(companyId: hitCompany.Company.Id, serverIP: hitCompany.ServerIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var actor =
			Setups.Users.Reza.Instance;

		actor.User.UpdateHash();

		DatabaseContext.Add(entity: actor.User);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: actor.User.Id, walletId: hitWallet.Wallet.Id)
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
				WalletToken = hitWallet.Token,
				CompanyToken = hitCompany.Token,
			};

		getBalanceRequest.User.CellPhoneNumber = actor.User.CellPhoneNumber;

		var getBalanceValue =
			Tasks.UsersControllerTasks.CallGetBalanceApiTask
			.Create(serverIP: hitCompany.ServerIP, databaseContext: DatabaseContext)
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
			.WithWalletToken(walletToken: hitWallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Token)
			.WithWithdrawDurationInDays(durationInDays: Setups.Constants.Shared.WithdrawDurationInDays)
			.WithUser(current => current.WithIP(ip: actor.IP).WithCellPhoneNumber(cellPhoneNumber: actor.User.CellPhoneNumber))
			.Build();

		var depositeValue =
			Tasks.UsersControllerTasks.CallDepositeApiTask
			.Create(serverIP: hitCompany.ServerIP, databaseContext: DatabaseContext)
			.SendRequest(request: depositeRequest);

		Assert.NotNull(@object: depositeValue);

		Assert.True(condition: depositeValue.IsSuccess);

		Assert.Equal(expected: 0, actual: depositeValue.ErrorMessages.Count);

		Assert.NotNull(@object: depositeValue.Data);

		Assert.Equal
			(expected: depositeRequest.Amount, actual: depositeValue.Data.Balance);
		// **************************************************

		// **************************************************
		var paymentRequest =
			Builders.PaymentRequestBuilder.Create()
			.WithAmount(amount: paymentAmount)
			.WithWalletToken(walletToken: hitWallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Token)
			.WithUser(current => current.WithIP(ip: actor.IP)
				.WithCellPhoneNumber(cellPhoneNumber: actor.User.CellPhoneNumber))
			.Build();

		var paymentValue =
			Tasks.UsersControllerTasks.CallPaymentApiTask
			.Create(serverIP: hitCompany.ServerIP, databaseContext: DatabaseContext)
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
}
