namespace Dtat.Wallet.Abstractions
{
	public enum TransactionType : int
	{
		/// <summary>
		/// پرداخت از طریق کیف پول برای خرید
		/// </summary>
		Payment = 0,

		/// <summary>
		/// واریز به کیف پول از حساب بانکی
		/// </summary>
		Deposite = 1,

		/// <summary>
		/// برداشت از کیف پول و واریز به حساب بانکی
		/// </summary>
		Withdraw = 2,

		/// <summary>
		/// انتقال به غیر
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//Transfer = 3,
	}
}
