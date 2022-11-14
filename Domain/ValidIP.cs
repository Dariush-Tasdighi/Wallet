namespace Domain;

public class ValidIP : Seedwork.Entity, Dtat.Wallet.Abstractions.IValidIP<long>
{
	#region Constructor
	public ValidIP(string serverIP) : base()
	{
		ServerIP = serverIP;
	}
	#endregion /Constructor

	#region Properties

	public long WalletId { get; private set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
	public string ServerIP { get; set; }

	public int RequestCount { get; set; }

	public System.DateTime? LastRequestDateTime { get; set; }

	#endregion /Properties
}
