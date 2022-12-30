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
	internal WithdrawRequestUserBuilder User { get; set; }
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
