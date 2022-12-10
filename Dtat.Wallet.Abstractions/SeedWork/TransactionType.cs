namespace Dtat.Wallet.Abstractions.SeedWork;

public enum TransactionType : int
{
	/// <summary>
	/// تعریف نشده
	/// </summary>
	Undefined = 0,

	/// <summary>
	/// واریز به کیف پول از حساب بانکی
	/// </summary>
	Deposite = 1,

	/// <summary>
	/// پرداخت از طریق کیف پول برای خرید
	/// </summary>
	Payment = 2,

	/// <summary>
	/// برگشت به کیف پول
	/// </summary>
	Refund = 3,

	/// <summary>
	/// برداشت از کیف پول و واریز به حساب بانکی
	/// </summary>
	Withdraw = 4,

	/// <summary>
	/// انتقال به غیر
	/// </summary>
	Transfer = 5,
}
