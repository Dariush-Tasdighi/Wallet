namespace Dtat.Wallet.Abstractions;

public interface ICompany<T> :
	SeedWork.IEntity<T>, SeedWork.IHasUpdateDateTime,
	SeedWork.IHasIsActive, SeedWork.IHasToken where T : struct
{
	string Name { get; }

	string? Description { get; }

	string? AdditionalData { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IValidIP<T>> ValidIPs { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ICompanyWallet<T>> CompanyWallets { get; }
}
