namespace Tests.Helpers;

[Xunit.Collection
	(name: Constants.Shared.DatabaseCollection)]
public abstract class TestsBase : object
{
	#region Constructor(s)
	public TestsBase(Helpers.DatabaseFixture databaseFixture) : base()
	{
		DatabaseContext =
			databaseFixture.DatabaseContext;
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected string ServerIP;
	protected Domain.User Actor;
	protected Domain.Wallet Wallet;
	protected Domain.Company Company;

	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	protected Domain.User SetupActor(Domain.User actor)
	{
		// **************************************************
		actor.UpdateHash();

		DatabaseContext.Add(entity: actor);

		DatabaseContext.SaveChanges();
		// **************************************************

		return actor;
	}

	protected Domain.UserWallet
		SetupUserWallet(Domain.UserWallet userWallet)
	{
		// **************************************************
		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		return userWallet;
	}

	protected Domain.ValidIP
		SetupCompanyValidIP(Domain.ValidIP validIP)
	{
		// **************************************************
		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		return validIP;
	}

	protected Domain.CompanyWallet
		SetupCompanyWallet(Domain.CompanyWallet companyWallet)
	{
		// **************************************************
		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		return companyWallet;
	}

	protected Domain.Wallet SetupWallet
		(Domain.Wallet wallet, System.Guid? walletToken = null)
	{
		// **************************************************
		if (walletToken.HasValue == false)
		{
			walletToken =
				System.Guid.NewGuid();
		}

		wallet.UpdateToken
			(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		return wallet;
	}

	protected Domain.Company SetupCompany
		(Domain.Company company, System.Guid? companyToken = null)
	{
		// **************************************************
		if (companyToken.HasValue == false)
		{
			companyToken =
				System.Guid.NewGuid();
		}

		company.UpdateToken
			(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();
		// **************************************************

		return company;
	}

	protected abstract void Arrange();
}
