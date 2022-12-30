namespace Tests.Builders;

internal class DepositeRequestBuilder : object
{
	#region Create()
	internal static DepositeRequestBuilder Create()
	{
		return new DepositeRequestBuilder();
	}
	#endregion /Create()

	#region Constructor()
	private DepositeRequestBuilder() : base()
	{
		Amount =
			Helpers.Constants.Shared.Amount;

		ReferenceCode =
			Helpers.Utility.ReferenceCode;

		ProviderName =
			Helpers.Constants.Shared.IranKishProviderName;

		WithdrawDurationInDays =
			Helpers.Constants.Shared.WithdrawDurationInDays;

		User =
			DepositeRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	internal DepositeRequestUserBuilder User { get; set; }
	#endregion /User

	#region Amount
	internal decimal Amount { get; set; }
	#endregion /Amount

	#region WalletToken
	internal System.Guid WalletToken { get; set; }
	#endregion /WalletToken

	#region CompanyToken
	internal System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken

	#region WithdrawDurationInDays
	internal int? WithdrawDurationInDays { get; set; }
	#endregion /WithdrawDurationInDays

	#region ProviderName (PSP)
	internal string ProviderName { get; set; }
	#endregion /ProviderName

	#region ReferenceCode
	internal string ReferenceCode { get; set; }
	#endregion /ReferenceCode

	#region UserDescription
	internal string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	internal string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#region AdditionalData
	internal string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties

	#region Methods()
	internal DepositeRequestBuilder
		WithUser(System.Action<DepositeRequestUserBuilder> action)
	{
		action.Invoke(User);

		return this;
	}

	internal DepositeRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	internal DepositeRequestBuilder WithWithdrawDurationInDays(int? durationInDays)
	{
		WithdrawDurationInDays = durationInDays;

		return this;
	}

	internal DepositeRequestBuilder WithProviderName(string providerName)
	{
		ProviderName = providerName;

		return this;
	}

	internal DepositeRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	internal DepositeRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}

	internal DepositeRequestBuilder WithReferenceCode(string referenceCode)
	{
		ReferenceCode = referenceCode;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.DepositeRequestDto Build()
	{
		var user = User.Build();

		var request =
			new Dtos.Users.DepositeRequestDto()
			{
				User = user,
				Amount = Amount,
				WalletToken = WalletToken,
				ProviderName = ProviderName,
				CompanyToken = CompanyToken,
				ReferenceCode = ReferenceCode,
				AdditionalData = AdditionalData,
				UserDescription = UserDescription,
				SystemicDescription = SystemicDescription,
				WithdrawDurationInDays = WithdrawDurationInDays,
			};

		return request;
	}
	#endregion /Build()
}
