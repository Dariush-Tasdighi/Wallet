namespace Dtat.Wallet.Abstractions;

public interface ICompany<T> :
	SeedWork.IEntity<T>, SeedWork.IHasIsActive<T>, SeedWork.IHasToken<T> where T : struct
{
	string Name { get; }

	string? Description { get; }

	string? AdditionalData { get; }



	System.DateTime UpdateDateTime { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IValidIP<T>> ValidIPs { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ICompanyWallet<T>> CompanyWallets { get; }
}
