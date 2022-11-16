using Dtat.Wallet.Abstractions.SeedWork;

namespace Domain;

public class Company : Seedwork.Entity, Dtat.Wallet.Abstractions.ICompany<long>
{
	#region Constructor
	public Company(string name) : base()
	{
		Name = name;
		Token = System.Guid.NewGuid();
		UpdateDateTime = InsertDateTime;

		Wallets =
			new System.Collections.Generic.List<Wallet>();
	}
	#endregion /Constructor

	#region Properties

	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region Token
	public System.Guid Token { get; set; }
	#endregion /Token

	#region UpdateDateTime
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime



	#region Name
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constant.MaxLength.Name)]
	public string Name { get; set; }
	#endregion /Name

	#region Description
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constant.MaxLength.Description)]
	public string? Description { get; set; }
	#endregion /Description

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData



	#region Wallets
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Wallet> Wallets { get; private set; }
	#endregion /Wallets

	#endregion /Properties
}
