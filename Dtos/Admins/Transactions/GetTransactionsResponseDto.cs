namespace Dtos.Admins.Transactions;

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

	#region Transactions
	public System.Collections.Generic.IList<GetTransactionResponseDto> Items { get; set; }
	#endregion /Transactions

	#endregion /Properties
}
