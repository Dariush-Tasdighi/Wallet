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
			Helpers.Constants.Shared.Amount;

		ReferenceCode =
			Helpers.Utility.ReferenceCode;

		User =
			PaymentRequestUserBuilder.Create();
	}
	#endregion /Constructor(s)

	#region Properties

	#region User
	protected PaymentRequestUserBuilder User { get; private set; }
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
	internal PaymentRequestBuilder
		WithUser(System.Action<PaymentRequestUserBuilder> action)
		{
			action.Invoke(User);

			return this;
		}

	internal PaymentRequestBuilder WithAmount(decimal amount)
	{
		Amount = amount;

		return this;
	}

	internal PaymentRequestBuilder WithWalletToken(System.Guid walletToken)
	{
		WalletToken = walletToken;

		return this;
	}

	internal PaymentRequestBuilder WithCompanyToken(System.Guid companyToken)
	{
		CompanyToken = companyToken;

		return this;
	}

	internal PaymentRequestBuilder WithReferenceCode(string referenceCode)
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
