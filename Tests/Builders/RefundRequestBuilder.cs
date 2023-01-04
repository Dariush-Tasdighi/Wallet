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
		TransactionId = transactionId;

		Amount =
			Helpers.Constants.Shared.Amount;

		WithdrawDurationInDays =
			Helpers.Constants.Shared.WithdrawDurationInDays;

		User =
			RefundRequestUserBuilder.Create();
	}
	#endregion /Constructor()

	#region Properties

	#region User
	protected RefundRequestUserBuilder User { get; private set; }
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

	#region TransactionId
	protected long TransactionId { get; private set; }
	#endregion /TransactionId

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
	internal RefundRequestBuilder
		WithUser(System.Action<RefundRequestUserBuilder> action)
		{
			action.Invoke(User);

			return this;
		}

	internal RefundRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	internal RefundRequestBuilder WithWithdrawDurationInDays(int? withdrawDurationInDays)
	{
		WithdrawDurationInDays = withdrawDurationInDays;

		return this;
	}

	internal RefundRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	internal RefundRequestBuilder WithCompanyToken(System.Guid companyToken)
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
