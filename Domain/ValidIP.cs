namespace Domain;

public class ValidIP : Seedwork.Entity, Dtat.Wallet.Abstractions.IValidIP<long>
{
	#region Constructor
	public ValidIP(long companyId, string serverIP) : base()
	{
		ServerIP = serverIP;
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



	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region ServerIP
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string ServerIP { get; set; }
	#endregion /ServerIP

	#region Description
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
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



	#region UpdateDateTime
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime

	#region LastRequestDateTime
	public System.DateTime? LastRequestDateTime { get; set; }
	#endregion /LastRequestDateTime

	#endregion /Properties
}
