namespace Dtos.Users;

public class GetBalanceRequestUserDto : object
{
	#region Constructor
	public GetBalanceRequestUserDto() : base()
	{
		CellPhoneNumber = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	/// <summary>
	/// در این درخواست الزامی نیست
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Required
	//	(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string? IP { get; set; }
	#endregion /IP (User IP)

	#region CellPhoneNumber
	/// <summary>
	/// شماره تلفن همراه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.CellPhoneNumber)]
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#endregion /Properties
}
