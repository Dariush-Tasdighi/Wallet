namespace Dtos.Users;

public class GetBalanceResponseDto : Base.Response
{
	#region Constructor
	public GetBalanceResponseDto
		(decimal balance, decimal withdrawBalance) :
		base(balance: balance, withdrawBalance: withdrawBalance)
	{
	}
	#endregion /Constructor
}
