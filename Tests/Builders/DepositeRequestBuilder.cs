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
	protected DepositeRequestUserBuilder User { get; private set; }
	#endregion /User

	#region Amount
	protected decimal Amount { get; private set; }
	#endregion /Amount

	#region WalletToken
	protected System.Guid WalletToken { get; private set; }
	#endregion /WalletToken

	#region CompanyToken
	protected System.Guid CompanyToken { get; private set; }
	#endregion /CompanyToken

	#region WithdrawDurationInDays
	protected int? WithdrawDurationInDays { get; private set; }
	#endregion /WithdrawDurationInDays

	#region ProviderName (PSP)
	protected string ProviderName { get; private set; }
	#endregion /ProviderName

	#region ReferenceCode
	protected string ReferenceCode { get; private set; }
	#endregion /ReferenceCode

	#region UserDescription
	protected string? UserDescription { get; private set; }
	#endregion /UserDescription

	#region SystemicDescription
	protected string? SystemicDescription { get; private set; }
	#endregion /SystemicDescription

	#region AdditionalData
	protected string? AdditionalData { get; private set; }
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
