namespace Dtos.Users;

public class DepositeResponseDto : Base.Response
{
	#region Constructor
	public DepositeResponseDto
		(decimal balance, decimal withdrawBalance, long transactionId) :
		base(balance: balance, withdrawBalance: withdrawBalance, transactionId: transactionId)
	{
	}
	#endregion /Constructor
}
