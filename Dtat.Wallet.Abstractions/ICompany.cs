namespace Dtat.Wallet.Abstractions
{
	public interface ICompany<T> : IBaseEntity<T>
	{
		string Name { get; }

		bool IsActive { get; }

		System.Guid Token { get; }



		System.Collections.Generic.IList<ICompanyWallet<T>> Wallets { get; }
	}
}
