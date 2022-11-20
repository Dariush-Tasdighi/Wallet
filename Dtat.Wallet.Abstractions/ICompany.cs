using Dtat.Wallet.Abstractions.SeedWork;

namespace Dtat.Wallet.Abstractions;

public interface ICompany<T> : IEntity<T>, IHasToken<T>
{
	string Name { get; }

	string? Description { get; }

	string? AdditionalData { get; }



	bool IsActive { get; }

	System.DateTime UpdateDateTime { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IWallet<T>> Wallets { get; }
}
