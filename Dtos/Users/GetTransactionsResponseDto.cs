namespace Dtos.Users;

public class GetTransactionsResponseDto : object
{
	#region Constructor
	public GetTransactionsResponseDto() : base()
	{
		Items =
			new System.Collections.Generic.List<GetTransactionResponseDto>();
	}
	#endregion /Constructor

	#region Properties

	#region TotalCount
	public long TotalCount { get; set; }
	#endregion /TotalCount

	#region DepositeTotal
	public decimal DepositeTotalAmount { get; set; }
	#endregion /DepositeTotalAmount

	#region WithdrawTotal
	public decimal WithdrawTotalAmount { get; set; }
	#endregion /WithdrawTotal

	#region DepositeTotal
	public decimal DepositeCurrentItemsTotalAmount { get; set; }
	#endregion /DepositeTotal

	#region WithdrawTotal
	public decimal WithdrawCurrentItemsTotalAmount { get; set; }
	#endregion /WithdrawTotal

	#region Transactions
	public System.Collections.Generic.IList<GetTransactionResponseDto> Items { get; set; }
	#endregion /Transactions

	#endregion /Properties
}
