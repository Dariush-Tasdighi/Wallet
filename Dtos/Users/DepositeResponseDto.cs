namespace Dtos.Users
{
	public class DepositeResponseDto : object
	{
		public DepositeResponseDto
			(decimal balance, long transactionId) : base()
		{
			Balance = balance;
			TransactionId = transactionId;
		}

		public decimal Balance { get; }

		public long TransactionId { get; }
	}
}
