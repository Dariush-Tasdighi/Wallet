namespace Dtos.Users;

public class PaymentResponseDto : object
{
	#region Constructor
	public PaymentResponseDto
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
