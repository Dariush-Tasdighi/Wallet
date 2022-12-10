namespace Domain;

public class UserWallet :
	Seedwork.Entity, Dtat.Wallet.Abstractions.IUserWallet<long>
{
	#region Constructor
	public UserWallet(long userId, long walletId) : base()
	{
		UserId = userId;
		WalletId = walletId;

		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	#region UserId
	public long UserId { get; private set; }
	#endregion /UserId

	#region User
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual User? User { get; private set; }
	#endregion /User



	#region WalletId
	public long WalletId { get; private set; }
	#endregion /WalletId

	#region Wallet
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }
	#endregion /Wallet



	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region Balance
	public decimal Balance { get; set; }
	#endregion /Balance

	#region UpdateDateTime
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
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

		stringBuilder.Append
			(value: $"{nameof(InsertDateTime)}:{Dtat.ConvertForHashing.FromDateTime(value: InsertDateTime)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UserId)}:{UserId}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(WalletId)}:{WalletId}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(IsActive)}:{IsActive}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(Balance)}:{Dtat.ConvertForHashing.FromDecimal(value: Balance)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UpdateDateTime)}:{Dtat.ConvertForHashing.FromDateTime(value: UpdateDateTime)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(Description)}:{Dtat.ConvertForHashing.FromString(value: Description)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(AdditionalData)}:{Dtat.ConvertForHashing.FromString(value: AdditionalData)}");

		var text =
			stringBuilder.ToString();

		var result =
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
