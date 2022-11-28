namespace Dtos.Users;

public class PaymentRequestDto : object
{
	#region Constructor
	public PaymentRequestDto() : base()
	{
		ReferenceCode = string.Empty;
		User = new PaymentRequestUserDto();
	}
	#endregion /Constructor

	#region Properties

	#region User
	[System.ComponentModel.DataAnnotations.Required]
	public PaymentRequestUserDto User { get; set; }
	#endregion /User

	#region Amount
	[System.ComponentModel.DataAnnotations.Required]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 0, maximum: 500_000_000)]
	public decimal Amount { get; set; }
	#endregion /Amount

	#region WaletToken
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid WaletToken { get; set; }
	#endregion /WaletToken

	#region CompanyToken
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken

	#region ReferenceCode
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.ReferenceCode)]
	public string ReferenceCode { get; set; }
	#endregion /ReferenceCode

	#region UserDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties
}
