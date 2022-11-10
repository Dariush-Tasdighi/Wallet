namespace Dtat.Wallet.Abstractions
{
	public interface IWallet<T> : IBaseEntity<T>
	{
		T CompanyId { get; }



		/// <summary>
		/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
		/// این فیلد باید به صورت کاملا انگلیسی و بدون فاصله نوشته شود
		/// </summary>
		string Name { get; }

		/// <summary>
		/// این فیلد الزامی است
		/// </summary>
		string DisplayName { get; }



		bool IsActive { get; }

		string ValidIPs { get; }

		System.Guid Token { get; }

		System.DateTime UpdateDateTime { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }



		System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

		System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
	}
}
