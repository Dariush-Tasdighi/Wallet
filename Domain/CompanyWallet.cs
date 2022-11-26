namespace Domain;

public class CompanyWallet : Seedwork.Entity, Dtat.Wallet.Abstractions.ICompanyWallet<long>
{
	#region Constructor
	public CompanyWallet(long companyId, long walletId) : base()
	{
		WalletId = walletId;
		CompanyId = companyId;

		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	#region CompanyId
	public long CompanyId { get; private set; }
	#endregion /CompanyId

	#region Company
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Company? Company { get; private set; }
	#endregion /Company



	#region WalletId
	public long WalletId { get; private set; }
	#endregion /WalletId

	#region Wallet
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }
	#endregion /Wallet



	#region IsOwner
	public bool IsOwner { get; set; }
	#endregion /IsOwner

	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region UpdateDateTime
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



	#region Hash
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Hash)]
	public string? Hash { get; private set; }
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

	#endregion /Properties

	#region Methods

	public string GetHash()
	{
		var stringBuilder =
			new System.Text.StringBuilder();

		stringBuilder.Append($"{nameof(InsertDateTime)}:{InsertDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(CompanyId)}:{CompanyId}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(WalletId)}:{WalletId}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(IsActive)}:{IsActive}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(UpdateDateTime)}:{UpdateDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(Description)}:{Description}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(AdditionalData)}:{AdditionalData}");

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
