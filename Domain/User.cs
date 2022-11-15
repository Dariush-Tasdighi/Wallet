namespace Domain;

public class User : Seedwork.Entity, Dtat.Wallet.Abstractions.IUser<long>
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
	}
	#endregion /Constructor

	#region Properties

	public bool IsActive { get; set; }

	public System.DateTime UpdateDateTime { get; private set; }



	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.DisplayName)]
	public string DisplayName { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.CellPhoneNumber)]
	public string CellPhoneNumber { get; set; }



	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Hash)]
	public string? Hash { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? Description { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.EmailAddress)]
	public string? EmailAddress { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.NationalCode)]
	public string? NationalCode { get; set; }



	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Transaction> Transactions { get; private set; }

	#endregion /Properties
}
