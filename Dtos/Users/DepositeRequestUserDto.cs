namespace Dtos.Users;

public class DepositeRequestUserDto : object
{
	#region Constructor
	public DepositeRequestUserDto() : base()
	{
		IP = string.Empty;
		DisplayName = string.Empty;
		CellPhoneNumber = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	#region IP (User IP)
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string IP { get; set; }
	#endregion /IP (User IP)

	#region DisplayName (Full Name)
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.DisplayName)]
	public string DisplayName { get; set; }
	#endregion /DisplayName (Full Name)

	#region CellPhoneNumber
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.CellPhoneNumber)]
	public string CellPhoneNumber { get; set; }
	#endregion /CellPhoneNumber

	#region EmailAddress
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.EmailAddress)]
	public string? EmailAddress { get; set; }
	#endregion /EmailAddress

	#region NationalCode
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.NationalCode)]
	public string? NationalCode { get; set; }
	#endregion /NationalCode

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#region PaymentFeatureIsEnabled
	public bool PaymentFeatureIsEnabled { get; set; }
	#endregion /PaymentFeatureIsEnabled

	#region DepositeFeatureIsEnabled
	public bool DepositeFeatureIsEnabled { get; set; }
	#endregion /DepositeFeatureIsEnabled

	#region WithdrawFeatureIsEnabled
	public bool WithdrawFeatureIsEnabled { get; set; }
	#endregion /WithdrawFeatureIsEnabled

	#region TransferFeatureIsEnabled
	public bool TransferFeatureIsEnabled { get; set; }
	#endregion /TransferFeatureIsEnabled

	#endregion /Properties
}
