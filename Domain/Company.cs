namespace Domain;

public class Company :
	Seedwork.Entity, Dtat.Wallet.Abstractions.ICompany<long>
{
	#region Constructor
	public Company(string name) : base()
	{
		Name = name;
		UpdateDateTime = InsertDateTime;

		UpdateToken();

		ValidIPs =
			new System.Collections.Generic.List<ValidIP>();

		CompanyWallets =
			new System.Collections.Generic.List<CompanyWallet>();
	}
	#endregion /Constructor

	#region Properties

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



	#region ValidIPs
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<ValidIP> ValidIPs { get; private set; }
	#endregion /ValidIPs

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
