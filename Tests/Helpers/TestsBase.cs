using Domain;
using Faker;
using static Tests.Setups.Constants.Shared;

namespace Tests.Helpers;

[Xunit.Collection
	(name: Setups.Constants.Shared.DatabaseCollection)]
public class TestsBase : object
{
	#region Constructor(s)
	public TestsBase(Helpers.DatabaseFixture databaseFixture) : base()
	{
		DatabaseContext =
			databaseFixture.DatabaseContext;
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	protected Domain.User SetupActor
		(bool isActive = true, bool isVerified = true,
		string displayName = Setups.Constants.Shared.Actor.Reza,
		string nationalCode = Setups.Constants.Shared.Actor.NationalCode,
		string emailAddress = Setups.Constants.Shared.Actor.EmailAddress,
		string cellPhoneNumber = Setups.Constants.Shared.Actor.CellPhoneNumber)
	{
		// **************************************************
		var actor =
			Builders.Models.UserBuilder.Create()
			.Named(displayName: displayName)
			.WithNationalCode(nationalCode: nationalCode)
			.WithEmailAddress(emailAddress: emailAddress)
			.WithCellPhoneNumber(cellPhoneNumber: cellPhoneNumber)
			.ThatIsActive(isActive: isActive)
			.ThatIsVerified(isVerified: isVerified)
			.Build();

		actor.UpdateHash();

		DatabaseContext.Add(entity: actor);

		DatabaseContext.SaveChanges();
		// **************************************************

		return actor;
	}

	protected Domain.UserWallet SetupUserWallet
		(long userId, long walletId, decimal balance = 0, bool isActive = true)
	{
		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: userId, walletId: walletId)
		{
			Balance = balance,
			IsActive = isActive,
		};

		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		return userWallet;
	}

	protected Domain.Company SetupCompany
		(string name = Setups.Constants.Shared.Company.Hit,
		System.Guid? companyToken = null, bool isActive = true)
	{
		// **************************************************
		var company =
			Builders.Models.CompanyBuilder.Create()
			.Named(name: name)
			.ThatIsActive(isActive: isActive)
			.Build();

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

	protected Domain.ValidIP
		SetupCompanyValidIP
		(long companyId, bool isActive = true,
		string serverIP = Setups.Constants.Shared.Company.ServerIP)
	{
		// **************************************************
		var validIP =
			new Domain.ValidIP
			(companyId: companyId, serverIP: serverIP)
			{
				IsActive = isActive,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		return validIP;
	}

	protected Domain.CompanyWallet SetupCompanyWallet
		(long companyId, long walletId, bool isActive = true)
	{
		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: companyId, walletId: walletId)
		{
			IsActive = isActive,
		};

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
}
