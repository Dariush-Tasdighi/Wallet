namespace Dtat.Wallet.Abstractions
{
	public interface IUserWallet<T> : IBaseEntity<T>
	{
		T UserId { get; }

		T WalletId { get; }



		bool IsActive { get; }


		string? Hash { get; }

		string? Description { get; }

		string? AdditionalData { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		/// <summary>
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//bool TransferFeatureIsEnabled { get; }



		System.DateTime UpdateDateTime { get; }

        void Deposit(decimal amount);
        decimal GetBalance();
        decimal Withdraw(decimal amount);
    }
}
