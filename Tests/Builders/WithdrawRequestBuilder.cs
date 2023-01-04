namespace Tests.Builders;

internal class WithdrawRequestBuilder : object
{
	#region Create()
	internal static WithdrawRequestBuilder Create()
	{
		return new WithdrawRequestBuilder();
	}
	#endregion /Create()

	#region Constructor()
	private WithdrawRequestBuilder() : base()
	{
		Amount =
			Helpers.Constants.Shared.Amount;

		User =
			WithdrawRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	protected WithdrawRequestUserBuilder User { get; private set; }
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
	
	internal WithdrawRequestBuilder
		WithUser(System.Action<WithdrawRequestUserBuilder> action)
		{
			action.Invoke(User);

			return this;
		}

	internal WithdrawRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	internal WithdrawRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	internal WithdrawRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.WithdrawRequestDto Build()
	{
		var user = User.Build();

		var request =
			new Dtos.Users.WithdrawRequestDto()
			{
				User = user,
				Amount = Amount,
				WalletToken = WalletToken,
				CompanyToken = CompanyToken,
				AdditionalData = AdditionalData,
				UserDescription = UserDescription,
				SystemicDescription = SystemicDescription,
			};

		return request;
	}
	#endregion /Build()
}
