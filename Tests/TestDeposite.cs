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

		hitWallet.Wallet.DepositeFeatureIsEnabled = true;

		hitWallet.Wallet.UpdateToken(token: hitWallet.Token);

		DatabaseContext.Add(entity: hitWallet.Wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var hitCompany =
			Setups.Company.Hit.Instance;

		hitCompany.Company.UpdateToken(token: hitCompany.Token);

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
			.WithWalletToken(walletToken: hitWallet.Wallet.Token)
			.WithCompanyToken(companyToken: hitCompany.Company.Token)
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
			(expected: expectedBalance, actual: depositeValue.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /DoDeposite()
}
