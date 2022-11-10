namespace Dtat.Wallet.Abstractions
{
	public interface ICompany<T> : IBaseEntity<T>
	{
		string Name { get; }

		string? Description { get; }



		bool IsActive { get; }

		System.Guid Token { get; }

		System.DateTime UpdateDateTime { get; }



		System.Collections.Generic.IList<ICompanyWallet<T>> Wallets { get; }
	}
}
