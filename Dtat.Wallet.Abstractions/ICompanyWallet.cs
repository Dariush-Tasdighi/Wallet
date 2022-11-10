namespace Dtat.Wallet.Abstractions
{
	public interface ICompanyWallet<T> : IBaseEntity<T>
	{
		string Name { get; }

		bool IsActive { get; }

		string ValidIPs { get; }

		System.Guid Token { get; }

		System.DateTime UpdateDateTime { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }



		System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

		System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
	}
}
