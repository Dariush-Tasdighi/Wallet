namespace Tests.Builders;

internal class PaymentRequestBuilder : object
{
	#region Create()
	internal static PaymentRequestBuilder Create()
	{
		return new PaymentRequestBuilder();
	}
	#endregion /Create()

	#region Constructor(s)
	private PaymentRequestBuilder() : base()
	{
		Amount =
			Setups.Constants.Shared.Amount;

		ReferenceCode =
			Helpers.Utility.ReferenceCode;

		User =
			PaymentRequestUserBuilder.Create();
	}
	#endregion /Constructor(s)

	#region Properties

	#region User
	public PaymentRequestUserBuilder User { get; set; }
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
	public PaymentRequestBuilder
		WithUser(System.Action<PaymentRequestUserBuilder> action)
		{
			action.Invoke(User);

			return this;
		}

	public PaymentRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	public PaymentRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	public PaymentRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}

	public PaymentRequestBuilder WithReferenceCode(string referenceCode)
	{
		ReferenceCode = referenceCode;

		return this;
	}
	#endregion /Methods()

	#region Build()
	internal Dtos.Users.PaymentRequestDto Build()
	{
		var user = User.Build();

		var request =
			new Dtos.Users.PaymentRequestDto()
			{
				User = user,
				Amount = Amount,
				WalletToken = WalletToken,
				CompanyToken = CompanyToken,
				ReferenceCode = ReferenceCode,
				AdditionalData = AdditionalData,
				UserDescription = UserDescription,
				SystemicDescription = SystemicDescription,
			};

		return request;
	}
	#endregion /Build()
}
