namespace Dtat.Wallet.Abstractions
{
	public interface IUserWallet<T> : IBaseEntity<T>
	{
		T UserId { get; }

		T WalletId { get; }



		bool IsActive { get; }

		decimal Balance { get; }

		System.DateTime UpdateDateTime { get; }

		/// <summary>
		/// فیلد مربوط به شناسه‌کاربر در سازمان مربوطه
		/// این فیلد می‌تواند شماره تلفن همراه کاربر باشد
		/// </summary>
		string CompanyUserIdentity { get; }



		string? Hash { get; }

		string? Description { get; }

		string? AdditionalData { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }
	}
}
