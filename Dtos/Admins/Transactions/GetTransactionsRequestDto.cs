namespace Dtos.Admins.Transactions;

public class GetTransactionsRequestDto : Shared.Pagination
{
	#region Constructor
	public GetTransactionsRequestDto() : base()
	{
		User = new();
	}
	#endregion /Constructor

	#region Properties

	#region Properties

	#region User
	/// <summary>
	/// اطلاعات کاربر
	/// </summary>
	public GetTransactionRequestUserDto User { get; set; }
	#endregion /User



	#region Type
	public Dtat.Wallet.Abstractions.SeedWork.TransactionType? Type { get; set; }
	#endregion /Type



	#region WalletToken
	/// <summary>
	/// توکن کیف پول
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid WalletToken { get; set; }
	#endregion /WalletToken

	#region CompanyToken
	/// <summary>
	/// توکن شرکت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken



	#region ToDate
	public System.DateTime? ToDate { get; set; }
	#endregion /ToDate

	#region FromDate
	public System.DateTime? FromDate { get; set; }
	#endregion /FromDate



	#region MinimumAmount
	public decimal? MinimumAmount { get; set; }
	#endregion /MinimumAmount

	#region MaximumAmount
	public decimal? MaximumAmount { get; set; }
	#endregion /MaximumAmount

	#endregion /Properties

	#endregion /Properties
}
