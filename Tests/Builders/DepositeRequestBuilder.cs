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
			Setups.Constants.Shared.Amount;

		ReferenceCode =
			Helpers.Utility.ReferenceCode;

		ProviderName =
			Setups.Constants.Shared.IranKishProviderName;

		WithdrawDurationInDays =
			Setups.Constants.Shared.WithdrawDurationInDays;

		User =
			DepositeRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	public DepositeRequestUserBuilder User { get; set; }
	#endregion /User

	#region Amount
	public decimal Amount { get; set; }
	#endregion /Amount

	#region WalletToken
	public System.Guid WalletToken { get; set; }
	#endregion /WalletToken

	#region CompanyToken
	public System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken

	#region WithdrawDurationInDays
	public int? WithdrawDurationInDays { get; set; }
	#endregion /WithdrawDurationInDays

	#region ProviderName (PSP)
	public string ProviderName { get; set; }
	#endregion /ProviderName

	#region ReferenceCode
	public string ReferenceCode { get; set; }
	#endregion /ReferenceCode

	#region UserDescription
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#region AdditionalData
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties

	#region Methods()
	public DepositeRequestBuilder
		WithUser(System.Action<DepositeRequestUserBuilder> action)
	{
		action.Invoke(User);

		return this;
	}

	public DepositeRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	public DepositeRequestBuilder WithWithdrawDurationInDays(int? durationInDays)
	{
		WithdrawDurationInDays = durationInDays;

		return this;
	}

	public DepositeRequestBuilder WithProviderName(string providerName)
	{
		ProviderName = providerName;

		return this;
	}

	public DepositeRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	public DepositeRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}

	public DepositeRequestBuilder WithReferenceCode(string referenceCode)
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
