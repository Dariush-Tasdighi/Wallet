namespace Domain;

public class Wallet : Seedwork.Entity, Dtat.Wallet.Abstractions.IWallet<long>
{
	#region Constructor
	public Wallet(string name) : base()
	{
		Name = name;

		UpdateToken();

		UpdateDateTime = InsertDateTime;

		UserWallets =
			new System.Collections.Generic.List<UserWallet>();

		Transactions =
			new System.Collections.Generic.List<Transaction>();

		CompanyWallets =
			new System.Collections.Generic.List<CompanyWallet>();
	}
	#endregion /Constructor

	#region Properties

	#region Name
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Name)]
	public string Name { get; set; }
	#endregion /Name



	#region Hash
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Hash)]
	public string? Hash { get; set; }
	#endregion /Hash

	#region Description
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? Description { get; set; }
	#endregion /Description

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData



	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region Token
	public System.Guid Token { get; private set; }
	#endregion /Token

	#region UpdateDateTime
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



	#region PaymentFeatureIsEnabled
	public bool PaymentFeatureIsEnabled { get; set; }
	#endregion /PaymentFeatureIsEnabled

	#region DepositeFeatureIsEnabled
	public bool DepositeFeatureIsEnabled { get; set; }
	#endregion /DepositeFeatureIsEnabled

	#region WithdrawFeatureIsEnabled
	public bool WithdrawFeatureIsEnabled { get; set; }
	#endregion /WithdrawFeatureIsEnabled

	#region TransferFeatureIsEnabled
	public bool TransferFeatureIsEnabled { get; set; }
	#endregion /TransferFeatureIsEnabled



	#region UserWallets
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }
	#endregion /UserWallets

	#region Transactions
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Transaction> Transactions { get; private set; }
	#endregion /Transactions

	#region CompanyWallets
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<CompanyWallet> CompanyWallets { get; private set; }
	#endregion /CompanyWallets

	#endregion /Properties

	#region Methods

	public void UpdateToken(System.Guid? token = null)
	{
		if (token.HasValue == false)
		{
			token =
				System.Guid.NewGuid();
		}

		Token = token.Value;
	}

	public string GetHash()
	{
		var stringBuilder =
			new System.Text.StringBuilder();

		stringBuilder.Append($"{nameof(InsertDateTime)}:{InsertDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(Name)}:{Name}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(Description)}:{Description}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(AdditionalData)}:{AdditionalData}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(IsActive)}:{IsActive}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(Token)}:{Token}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(UpdateDateTime)}:{UpdateDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(PaymentFeatureIsEnabled)}:{PaymentFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(DepositeFeatureIsEnabled)}:{DepositeFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(WithdrawFeatureIsEnabled)}:{WithdrawFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(TransferFeatureIsEnabled)}:{TransferFeatureIsEnabled}");

		var text =
			stringBuilder.ToString();

		string result =
			Dtat.Utility.GetSha256(text: text);

		return result;
	}

	public void UpdateHash()
	{
		Hash = GetHash();
	}

	public bool CheckHashValidation()
	{
		var result = GetHash();

		if (string.Compare(result, Hash, ignoreCase: true) == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	#endregion /Methods
}
