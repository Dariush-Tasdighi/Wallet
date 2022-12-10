namespace Domain;

public class InvalidRequestLog :
	Seedwork.Entity, Dtat.Wallet.Abstractions.IInvalidRequestLog<long>
{
	#region Constructor
	public InvalidRequestLog(string serverIP) : base()
	{
		ServerIP = serverIP;
	}
	#endregion /Constructor

	#region Properties

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



	#region WalletToken
	public System.Guid? WalletToken { get; set; }
	#endregion /WalletToken

	#region CompanyToken
	public System.Guid? CompanyToken { get; set; }
	#endregion /CompanyToken

	#region CellPhoneNumber
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.CellPhoneNumber)]
	public string? CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#endregion /Properties
}
