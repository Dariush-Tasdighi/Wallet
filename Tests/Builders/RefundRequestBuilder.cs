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
	internal RefundRequestUserBuilder User { get; set; }
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

	#region TransactionId
	internal long TransactionId { get; set; }
	#endregion /TransactionId

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
