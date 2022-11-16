namespace Dtos.Users;

public class DepositeRequestDto : object
{
	#region Constructor
	public DepositeRequestDto() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region User
	[System.ComponentModel.DataAnnotations.Required]
	public UserDto? User { get; set; }
	#endregion /User

	#region Amount
	[System.ComponentModel.DataAnnotations.Range
		(minimum: 0, maximum: 500_000_000)]
	public decimal Amount { get; set; }
	#endregion /Amount

	#region WaletToken
	public System.Guid WaletToken { get; set; }
	#endregion /WaletToken

	#region ProviderName (PSP)
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ProviderName)]
	public string? ProviderName { get; set; }
	#endregion /ProviderName

	#region ReferenceCode
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ReferenceCode)]
	public string? ReferenceCode { get; set; }
	#endregion /ReferenceCode

	#region UserDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties
}
