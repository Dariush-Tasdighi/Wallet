namespace Dtos.Users.Base;

public abstract class Response : object
{
	#region Constructor
	public Response
		(decimal balance, decimal withdrawBalance) : base()
	{
		Balance = balance;
		WithdrawBalance = withdrawBalance;

	}
	#endregion /Constructor

	#region Properties

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
