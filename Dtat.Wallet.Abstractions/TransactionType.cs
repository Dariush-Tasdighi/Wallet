namespace Dtat.Wallet.Abstractions
{
	public enum TransactionType : int
	{
		/// <summary>
		/// پرداخت از طریق کیف پول برای خرید
		/// </summary>
		Payment,

		/// <summary>
		/// واریز به کیف پول از حساب بانکی
		/// </summary>
		Deposite,

		/// <summary>
		/// برداشت از کیف پول و واریز به حساب بانکی
		/// </summary>
		Withdraw,

		/// <summary>
		/// انتقال به غیر
		/// </summary>
		Transfer,
	}
}
