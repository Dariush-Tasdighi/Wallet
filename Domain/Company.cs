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

	public bool IsActive { get; set; }

	public System.Guid Token { get; set; }

	public System.DateTime UpdateDateTime { get; private set; }



	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Name)]
	public string Name { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? Description { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual System.Collections.Generic.IList<Wallet> Wallets { get; private set; }

	#endregion /Properties
}
