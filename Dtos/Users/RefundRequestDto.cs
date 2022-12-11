namespace Dtos.Users;

public class RefundRequestDto : object
{
	#region Constructor
	public RefundRequestDto() : base()
	{
		User = new();
	}
	#endregion /Constructor

	#region Properties

	#region User
	/// <summary>
	/// اطلاعات کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public RefundRequestUserDto User { get; set; }
	#endregion /User

	#region Amount
	/// <summary>
	/// مبلغ
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 0, maximum: 500_000_000)]
	public decimal Amount { get; set; }
	#endregion /Amount

	#region WalletToken
	/// <summary>
	/// توکن کیف پول
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid WalletToken { get; set; }
	#endregion /WalletToken

	#region CompanyToken
	/// <summary>
	/// توکن شرکت/سازمان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken

	#region WithdrawDurationInDays
	/// <summary>
	/// تا چند روز آتی امکان برداشت وجه وجود دارد
	/// باشد امکان برداشت وجه وجود ندارد null در صورتی که
	/// </summary>
	public int? WithdrawDurationInDays { get; set; }
	#endregion /WithdrawDurationInDays

	#region TransactionId
	/// <summary>
	/// شماره تراکنش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public long TransactionId { get; set; }
	#endregion /TransactionId

	#region UserDescription
	/// <summary>
	/// توضیحات کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	/// <summary>
	/// توضیحات سیستمی
	/// </summary>
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

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
