namespace Domain;

public class User :
	Seedwork.Entity, Dtat.Wallet.Abstractions.IUser<long>
{
	#region Constructor
	public User(string cellPhoneNumber, string displayName) : base()
	{
		DisplayName = displayName;
		UpdateDateTime = InsertDateTime;
		CellPhoneNumber = cellPhoneNumber;

		UserWallets =
			new System.Collections.Generic.List<UserWallet>();

		Transactions =
			new System.Collections.Generic.List<Transaction>();

		PartyTransactions =
			new System.Collections.Generic.List<Transaction>();
	}
	#endregion /Constructor

	#region Properties

	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region IsVerified
	public bool IsVerified { get; set; }
	#endregion /IsVerified

	#region UpdateDateTime
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



	#region DisplayName
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.DisplayName)]
	public string DisplayName { get; set; }
	#endregion /DisplayName

	#region CellPhoneNumber
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.CellPhoneNumber)]
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber



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

	#region EmailAddress
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.EmailAddress)]
	public string? EmailAddress { get; set; }
	#endregion /EmailAddress

	#region NationalCode
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.NationalCode)]
	public string? NationalCode { get; set; }
	#endregion /NationalCode



	#region UserWallets
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }
	#endregion /UserWallets

	#region Transactions
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Transaction> Transactions { get; private set; }
	#endregion /Transactions

	#region PartyTransactions
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Transaction> PartyTransactions { get; private set; }
	#endregion /PartyTransactions

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
			(value: $"{nameof(IsActive)}:{IsActive}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(IsVerified)}:{IsVerified}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UpdateDateTime)}:{Dtat.ConvertForHashing.FromDateTime(value: UpdateDateTime)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(DisplayName)}:{Dtat.ConvertForHashing.FromString(value: DisplayName)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(CellPhoneNumber)}:{Dtat.ConvertForHashing.FromString(value: CellPhoneNumber)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(Description)}:{Dtat.ConvertForHashing.FromString(value: Description)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(EmailAddress)}:{Dtat.ConvertForHashing.FromString(value: EmailAddress)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(NationalCode)}:{Dtat.ConvertForHashing.FromString(value: NationalCode)}");

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
