namespace Dtat.Wallet.Abstractions;

public interface IWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasUpdateDateTime,
	SeedWork.IHasIsActive where T : struct
{
	/// <summary>
	/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
	/// </summary>
	string Name { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	bool RefundFeatureIsEnabled { get; }

	bool PaymentFeatureIsEnabled { get; }

	bool DepositeFeatureIsEnabled { get; }

	bool WithdrawFeatureIsEnabled { get; }

	bool TransferFeatureIsEnabled { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ICompanyWallet<T>> CompanyWallets { get; }
}
