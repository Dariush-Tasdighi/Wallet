namespace Domain;

public class Wallet :
	Seedwork.Entity, Dtat.Wallet.Abstractions.IWallet<long>
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



	#region Token
	public System.Guid Token { get; private set; }
	#endregion /Token

	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region UpdateDateTime
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



	#region RefundFeatureIsEnabled
	public bool RefundFeatureIsEnabled { get; set; }
	#endregion /RefundFeatureIsEnabled

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

	#endregion /Methods
}
