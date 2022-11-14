namespace Dtat.Wallet.Abstractions
{
	public interface IWallet<T> : IBaseEntity<T>
	{
		T CompanyId { get; }



		/// <summary>
		/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
		/// </summary>
		string Name { get; }



		string? Hash { get; }

		string? Description { get; }

		string? AdditionalData { get; }



		bool IsActive { get; }

		System.Guid Token { get; }

		System.DateTime UpdateDateTime { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		/// <summary>
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//bool TransferFeatureIsEnabled { get; }



		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<IValidIP<T>> ValidIPs { get; }

		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
	}
}
