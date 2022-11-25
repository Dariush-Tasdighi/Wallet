namespace Dtat.Wallet.Abstractions;

public interface IWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasToken<T>, SeedWork.IHashing<T> where T : struct
{
	T CompanyId { get; }



	bool IsActive { get; }



	/// <summary>
	/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
	/// </summary>
	string Name { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	bool PaymentFeatureIsEnabled { get; }

	bool DepositeFeatureIsEnabled { get; }

	bool WithdrawFeatureIsEnabled { get; }

	bool TransferFeatureIsEnabled { get; }



	System.DateTime UpdateDateTime { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IValidIP<T>> ValidIPs { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IInvalidIP<T>> InvalidIPs { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
}
