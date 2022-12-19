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

	#region Properties

	#region DepositeTotal
	public decimal DepositeTotalAmount { get; set; }
	#endregion /DepositeTotalAmount

	#region WithdrawTotal
	public decimal WithdrawTotalAmount { get; set; }
	#endregion /WithdrawTotal

	#endregion /Properties
}
