namespace Tests.Builders;

internal class GetBalanceRequestBuilder : object
{
	#region Create()
	internal static GetBalanceRequestBuilder Create()
	{
		return new GetBalanceRequestBuilder();
	}
	#endregion /Create()

	#region Constructor()
	private GetBalanceRequestBuilder() : base()
	{
		User =
			GetBalanceRequestUserBuilder.Create();

		WalletToken =
			Helpers.Constants.Shared.Wallet.Token;

		CompanyToken =
			Helpers.Constants.Shared.Company.Token;
	}
	#endregion /Constructor()

	#region Properties

	#region User
	protected GetBalanceRequestUserBuilder User { get; private set; }
	#endregion /User

	#region WalletToken
	protected System.Guid WalletToken { get; private set; }
	#endregion /WalletToken

	#region CompanyToken
	protected System.Guid CompanyToken { get; private set; }
	#endregion /CompanyToken

	#endregion /Properties

	#region Methods()
	internal GetBalanceRequestBuilder
		WithUser(System.Action<GetBalanceRequestUserBuilder> action)
	{
		action.Invoke(User);

		return this;
	}

	internal GetBalanceRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	internal GetBalanceRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.GetBalanceRequestDto Build()
	{
		var user = User.Build();

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				User = user,
				WalletToken = WalletToken,
				CompanyToken = CompanyToken,
			};

		return request;
	}
	#endregion /Build()
}
