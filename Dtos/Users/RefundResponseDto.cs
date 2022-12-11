namespace Dtos.Users;

public class RefundResponseDto : Base.Response
{
	#region Constructor
	public RefundResponseDto
		(decimal balance, decimal withdrawBalance, long transactionId) :
		base(balance: balance, withdrawBalance: withdrawBalance)
	{
		TransactionId = transactionId;
	}
	#endregion /Constructor

	#region Properties

	#region TransactionId
	/// <summary>
	/// شناسه آخرین تراکنش کاربر
	/// </summary>
	public long TransactionId { get; }
	#endregion /TransactionId

	#endregion /Properties
}
