namespace Dtat.Wallet.Abstractions;

public interface ICompany<T> :
	SeedWork.IEntity<T>, SeedWork.IHasToken<T> where T : struct
{
	string Name { get; }

	string? Description { get; }

	string? AdditionalData { get; }



	bool IsActive { get; }

	System.DateTime UpdateDateTime { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IWallet<T>> Wallets { get; }
}
