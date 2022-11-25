namespace Dtat.Wallet.Abstractions.SeedWork;

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
	/// </summary>
	Transfer = 3,
}
