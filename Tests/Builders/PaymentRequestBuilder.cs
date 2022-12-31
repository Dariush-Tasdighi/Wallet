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
	internal PaymentRequestUserBuilder User { get; set; }
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

	#region ReferenceCode
	internal string ReferenceCode { get; set; }
	#endregion /ReferenceCode

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
