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
			Setups.Constants.Shared.Amount;

		User =
			WithdrawRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	public WithdrawRequestUserBuilder User { get; set; }
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
	public WithdrawRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	public WithdrawRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	public WithdrawRequestBuilder WithCompanyToken(System.Guid companyToken)
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
