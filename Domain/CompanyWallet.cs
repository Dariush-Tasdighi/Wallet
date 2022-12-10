namespace Domain;

public class CompanyWallet :
	Seedwork.Entity, Dtat.Wallet.Abstractions.ICompanyWallet<long>
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
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



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
}
