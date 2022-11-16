namespace Dtos.Users;

public class DepositeResponseDto : object
{
	#region Constructor
	public DepositeResponseDto
		(decimal balance, long transactionId) : base()
	{
		Balance = balance;
		TransactionId = transactionId;
	}
	#endregion /Constructor

	#region Properties

	#region Balance
	public decimal Balance { get; }
	#endregion /Balance

	#region TransactionId
	public long TransactionId { get; }
	#endregion /TransactionId

	#endregion /Properties
}
