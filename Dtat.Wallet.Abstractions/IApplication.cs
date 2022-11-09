namespace Dtat.Wallet.Abstractions
{
	public interface IApplication<T> : IBaseEntity<T>
	{
		string Name { get; }

		string ValidIPs { get; }

		System.Guid Token { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }



		System.Collections.Generic.IReadOnlyList<IUser<T>> Users { get; }
	}
}
