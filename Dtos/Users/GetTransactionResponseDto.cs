namespace Dtos.Users;

public class GetTransactionResponseDto : object
{
	#region Constructor
	public GetTransactionResponseDto() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region UserId
	public long UserId { get; set; }
	#endregion /UserId

	#region WalletId
	public long WalletId { get; set; }
	#endregion /WalletId

	#region Amount
	public decimal Amount { get; set; }
	#endregion /Amount

	#region IsCleared
	/// <summary>
	/// تسویه شده
	/// </summary>
	public bool IsCleared { get; set; }
	#endregion /IsCleared

	#region Type
	public Dtat.Wallet.Abstractions.SeedWork.TransactionType Type { get; set; }
	#endregion /Type



	#region WithdrawDate
	public System.DateTime? WithdrawDate { get; set; }
	#endregion /WithdrawDate

	#region InsertDateTime
	public System.DateTime InsertDateTime { get; set; }
	#endregion /InsertDateTime



	#region PaymentReferenceCode
	public string? PaymentReferenceCode { get; set; }
	#endregion /PaymentReferenceCode

	#region DepositeOrWithdrawProviderName
	public string? DepositeOrWithdrawProviderName { get; set; }
	#endregion /DepositeOrWithdrawProviderName

	#region DepositeOrWithdrawReferenceCode
	public string? DepositeOrWithdrawReferenceCode { get; set; }
	#endregion /DepositeOrWithdrawReferenceCode



	#region UserIP
	public string? UserIP { get; set; }
	#endregion /UserIP

	#region AdditionalData
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#region UserDescription
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#endregion /Properties
}
