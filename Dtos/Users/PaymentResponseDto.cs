namespace Dtos.Users;

public class PaymentResponseDto : Base.Response
{
	#region Constructor
	public PaymentResponseDto
		(decimal balance, decimal withdrawBalance, long transactionId) :
		base(balance: balance, withdrawBalance: withdrawBalance, transactionId: transactionId)
	{
	}
	#endregion /Constructor
}
