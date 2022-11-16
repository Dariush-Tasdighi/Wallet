using Dtat.Wallet.Abstractions.SeedWork;

namespace Domain;

public class InvalidIP : Seedwork.Entity, Dtat.Wallet.Abstractions.IInvalidIP<long>
{
	#region Constructor
	public InvalidIP(long walletId, string serverIP) : base()
	{
		ServerIP = serverIP;
		WalletId = walletId;
		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	#region WalletId
	public long WalletId { get; private set; }
	#endregion /WalletId

	#region Wallet
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }
	#endregion /Wallet



	#region ServerIP
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constant.MaxLength.IP)]
	public string ServerIP { get; set; }
	#endregion /ServerIP

	#region Description
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constant.MaxLength.Description)]
	public string? Description { get; set; }
	#endregion /Description



	#region TotalRequestCount
	public int TotalRequestCount { get; set; }
	#endregion /TotalRequestCount

	#region CurrentDayRequestCount
	public int CurrentDayRequestCount { get; set; }
	#endregion /CurrentDayRequestCount

	#region PreviousDay1RequestCount
	public int PreviousDay1RequestCount { get; set; }
	#endregion /PreviousDay1RequestCount

	#region PreviousDay2RequestCount
	public int PreviousDay2RequestCount { get; set; }
	#endregion /PreviousDay2RequestCount

	#region PreviousDay3RequestCount
	public int PreviousDay3RequestCount { get; set; }
	#endregion /PreviousDay3RequestCount

	#region PreviousDay4RequestCount
	public int PreviousDay4RequestCount { get; set; }
	#endregion /PreviousDay4RequestCount

	#region PreviousDay5RequestCount
	public int PreviousDay5RequestCount { get; set; }
	#endregion /PreviousDay5RequestCount

	#region PreviousDay6RequestCount
	public int PreviousDay6RequestCount { get; set; }
	#endregion /PreviousDay6RequestCount



	#region LastRequestDateTime
	public System.DateTime? LastRequestDateTime { get; set; }
	#endregion /LastRequestDateTime

	#region UpdateDateTime
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime

	#endregion /Properties
}
