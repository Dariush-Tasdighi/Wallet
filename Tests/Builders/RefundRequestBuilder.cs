namespace Tests.Builders;

internal class RefundRequestBuilder : object
{
	#region Create()
	internal static RefundRequestBuilder Create(long transactionId)
	{
		return new RefundRequestBuilder(transactionId: transactionId);
	}
	#endregion /Create()

	#region Constructor()
	private RefundRequestBuilder(long transactionId) : base()
	{
		Amount =
			Setups.Constants.Shared.Amount;

		TransactionId = transactionId;

		WithdrawDurationInDays =
			Setups.Constants.Shared.WithdrawDurationInDaysNeutralValue;

		User =
			RefundRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	public RefundRequestUserBuilder User { get; set; }
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

	#region TransactionId
	public long TransactionId { get; set; }
	#endregion /TransactionId

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
	public RefundRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	public RefundRequestBuilder WithWithdrawDurationInDays(int? withdrawDurationInDays)
	{
		WithdrawDurationInDays = withdrawDurationInDays;

		return this;
	}

	public RefundRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	public RefundRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.RefundRequestDto Build()
	{
		var user = User.Build();

		var request =
			new Dtos.Users.RefundRequestDto()
			{
				User = user,
				Amount = Amount,
				WalletToken = WalletToken,
				CompanyToken = CompanyToken,
				TransactionId = TransactionId,
				AdditionalData = AdditionalData,
				UserDescription = UserDescription,
				SystemicDescription = SystemicDescription,
				WithdrawDurationInDays = WithdrawDurationInDays,
			};

		return request;
	}
	#endregion /Build()
}
