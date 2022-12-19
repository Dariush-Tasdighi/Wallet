namespace Dtos.Admins.Transactions;

public class GetTransactionRequestUserDto : object
{
	#region Constructor
	public GetTransactionRequestUserDto() : base()
	{
		CellPhoneNumber = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	/// <summary>
	/// آی‌پی کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string? IP { get; set; }
	#endregion /IP (User IP)

	#region CellPhoneNumber
	/// <summary>
	/// شماره تلفن همراه
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.CellPhoneNumber)]
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#endregion /Properties
}
