namespace Dtat.Wallet.Abstractions
{
	public interface ICompany<T> : IBaseEntity<T>
	{
		string Name { get; }

		string? Description { get; }

		string? AdditionalData { get; }



		bool IsActive { get; }

		System.Guid Token { get; }

		System.DateTime UpdateDateTime { get; }



		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<IWallet<T>> Wallets { get; }
	}
}
