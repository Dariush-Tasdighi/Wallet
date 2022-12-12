namespace Dtos.Users;

public class GetTransactionRequestUserDto : object
{
	#region Constructor
	public GetTransactionRequestUserDto() : base()
	{
		CellPhoneNumber = string.Empty;
	}
	#endregion /Constructor

	#region Properties

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
