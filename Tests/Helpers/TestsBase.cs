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
	protected Data.DatabaseContext DatabaseContext { get; }
	#endregion /Property(ies)

	#region SetupActor()
	protected Domain.User SetupActor(Domain.User actor)
	{
		// **************************************************
		actor.UpdateHash();

		DatabaseContext.Add(entity: actor);

		DatabaseContext.SaveChanges();
		// **************************************************

		return actor;
	}
	#endregion /SetupActor()

	#region SetupUserWallet()
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
	#endregion /SetupUserWallet()

	#region SetupCompanyValidIP()
	protected Domain.ValidIP
		SetupCompanyValidIP(Domain.ValidIP validIP)
	{
		// **************************************************
		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		return validIP;
	}
	#endregion /SetupCompanyValidIP()

	#region SetupCompanyWallet()
	protected Domain.CompanyWallet
		SetupCompanyWallet(Domain.CompanyWallet companyWallet)
	{
		// **************************************************
		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		return companyWallet;
	}
	#endregion /SetupCompanyWallet()

	#region SetupWallet()
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
	#endregion /SetupWallet()

	#region SetupCompany()
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
	#endregion /SetupCompany()

	#region ArrangeWallet()
	protected virtual Domain.Wallet
		ArrangeWallet(System.Guid? walletToken = null,
		string name = Constants.Shared.Wallet.Hit, bool isActive = true,
		bool isRefundFeatureEnabled = true, bool isWithdrawFeatureIsEnabled = true,
		bool isPaymentFeatureEnabled = true, bool isDepositeFeatureEnabled = true)
	{
		// **************************************************
		var wallet =
			Builders.Models.WalletBuilder.Create()
			.Named(name: name)
			.ThatIsActive(isActive: isActive)
			.ThatRefundFeatureIsEnabled(isEnabled: isRefundFeatureEnabled)
			.ThatPaymentFeatureIsEnabled(isEnabled: isPaymentFeatureEnabled)
			.ThatDepositeFeatureIsEnabled(isEnabled: isDepositeFeatureEnabled)
			.ThatWithdrawFeatureIsEnabled(isEnabled: isWithdrawFeatureIsEnabled)
			.Build();

		if (walletToken.HasValue == false)
		{
			walletToken =
				System.Guid.NewGuid();
		}

		SetupWallet
			(wallet: wallet, walletToken: walletToken);
		// **************************************************

		return wallet;
	}
	#endregion /ArrangeWallet()

	#region ArrangeCompany()
	protected virtual Domain.Company ArrangeCompany
		(System.Guid? companyToken = null, bool isActive = true,
		string? additionalData = null, string? description = null,
		string? companyName = Helpers.Constants.Shared.Company.Hit)
	{
		// **************************************************
		if (companyToken.HasValue == false)
		{
			companyToken =
				System.Guid.NewGuid();
		}

		var company =
			Builders.Models.CompanyBuilder.Create()
			.Named(name: companyName)
			.ThatIsActive(isActive: isActive)
			.WithDescription(description: description)
			.WithAdditionalData(additionalData: additionalData)
			.Build();

		company =
			SetupCompany(company: company, companyToken: companyToken);
		// **************************************************

		return company;
	}
	#endregion /ArrangeCompany()

	#region ArrangeCompanyValidIP()
	protected virtual
		Domain.ValidIP ArrangeCompanyValidIP
		(long companyId, bool isActive = true,
		string? serverIP = Constants.Shared.Company.ServerIP)
	{
		// **************************************************
		var validIP = new Domain.ValidIP
			(companyId: companyId, serverIP: serverIP)
		{
			IsActive = isActive,
		};

		SetupCompanyValidIP(validIP: validIP);
		// **************************************************

		return validIP;
	}
	#endregion /ArrangeCompanyValidIP()

	#region ArrangeCompanyWallet()
	protected virtual Domain.CompanyWallet ArrangeCompanyWallet
		(long? companyId = null, long? walletId = null, bool isActive = true)
	{
		// **************************************************
		if (walletId.HasValue == false)
		{
			walletId =
				Utility.FakeId;
		}

		if (companyId.HasValue == false)
		{
			companyId =
				Utility.FakeId;
		}

		var companyWallet = new Domain.CompanyWallet
			(companyId: companyId.Value, walletId: walletId.Value)
		{
			IsActive = isActive,
		};

		SetupCompanyWallet(companyWallet: companyWallet);
		// **************************************************

		return companyWallet;
	}
	#endregion /ArrangeCompanyWallet()

	#region ArrangeUserWallet()
	protected virtual Domain.UserWallet ArrangeUserWallet
		(long? actorId = null, long? walletId = null, decimal balance = 0, bool isActive = true)
	{
		// **************************************************
		if (actorId.HasValue == false)
		{
			actorId =
				Utility.FakeId;
		}

		if (walletId.HasValue == false)
		{
			walletId =
				Utility.FakeId;
		}

		var userWallet = new Domain.UserWallet
			(userId: actorId.Value, walletId: walletId.Value)
		{
			Balance = balance,
			IsActive = isActive,
		};

		SetupUserWallet(userWallet: userWallet);
		// **************************************************

		return userWallet;
	}
	#endregion /ArrangeUserWallet()

	#region ArrangeActor()
	protected virtual
		Domain.User ArrangeActor
		(string? displayName = Constants.Shared.Actor.Reza,
		bool isActive = true, bool isVerified = true,
		bool fakeNationalCode = true, string? nationalCode = null,
		bool fakeCellPhoneNumber = true, string? cellPhoneNumber = null,
		string? emailAddress = null, string? additionalData = null, string? description = null)
	{
		if (fakeNationalCode)
		{
			nationalCode =
				Utility.FakeNationalCode;
		}

		if (fakeCellPhoneNumber)
		{
			cellPhoneNumber =
				Utility.FakeCellPhoneNumber;
		}

		// **************************************************
		var actor =
			Builders.Models.UserBuilder.Create()
			.Named(displayName: displayName)
			.WithNationalCode(nationalCode: nationalCode)
			.WithEmailAddress(emailAddress: emailAddress)
			.WithCellPhoneNumber(cellPhoneNumber: cellPhoneNumber)
			.WithDescription(description: description)
			.ThatIsActive(isActive: isActive)
			.ThatIsVerified(isVerified: isVerified)
			.Build();

		actor =
			SetupActor(actor: actor);
		// **************************************************

		return actor;
	}
	#endregion /ArrangeActor()
}
