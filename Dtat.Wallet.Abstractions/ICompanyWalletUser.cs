namespace Dtat.Wallet.Abstractions
{
	public interface ICompanyWalletUser<T> : IBaseEntity<T>
	{
		bool IsActive { get; }

		decimal Balance { get; }

		/// <summary>
		/// فیلد مربوط به شناسه‌کاربر در سازمان مربوطه
		/// </summary>
		string CompanyUserIdentity { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }



		System.Collections.Generic.IList<IUser<T>> Users { get; }
	}
}
