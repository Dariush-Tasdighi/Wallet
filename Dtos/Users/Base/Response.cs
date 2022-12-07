namespace Dtos.Users.Base;

public class Response : object
{
	#region Constructor
	public Response
		(decimal balance, decimal withdrawBalance, long transactionId) : base()
	{
		Balance = balance;
		TransactionId = transactionId;
		WithdrawBalance = withdrawBalance;

	}
	#endregion /Constructor

	#region Properties

	#region TransactionId
	/// <summary>
	/// شناسه تراکنش
	/// </summary>
	public long TransactionId { get; }
	#endregion /TransactionId

	#region Balance
	/// <summary>
	/// مبلغ کل قابل خرید
	/// </summary>
	public decimal Balance { get; }
	#endregion /Balance

	#region WithdrawBalance
	/// <summary>
	/// صرفا مبلغ قابل برداشت
	/// </summary>
	public decimal WithdrawBalance { get; }
	#endregion /WithdrawBalance

	#endregion /Properties

}
