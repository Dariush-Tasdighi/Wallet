namespace Dtos.Users;

public class RefundRequestUserDto : object
{
	#region Constructor
	public RefundRequestUserDto() : base()
	{
		IP = string.Empty;
		DisplayName = string.Empty;
		CellPhoneNumber = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	/// <summary>
	/// آی‌پی کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string IP { get; set; }
	#endregion /IP (User IP)

	#region DisplayName (Full Name)
	/// <summary>
	/// نام و نام خانوادگی کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.DisplayName)]
	public string DisplayName { get; set; }
	#endregion /DisplayName (Full Name)

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

	#region EmailAddress
	/// <summary>
	/// نشانی پست الکترونیکی
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.EmailAddress)]
	public string? EmailAddress { get; set; }
	#endregion /EmailAddress

	#region NationalCode
	/// <summary>
	/// کد ملی
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.NationalCode)]
	public string? NationalCode { get; set; }
	#endregion /NationalCode

	#region AdditionalData
	/// <summary>
	/// اطلاعات تکمیلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties
}
