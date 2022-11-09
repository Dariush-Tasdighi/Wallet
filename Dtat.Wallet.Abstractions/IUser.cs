namespace Dtat.Wallet.Abstractions
{
	public interface IUser<T> : IBaseEntity<T>
	{
		decimal Balance { get; }

		string ApplicationUserId { get; }



		string? Username { get; }

		string? LastName { get; }

		string? FirstName { get; }

		string? EmailAddress { get; }

		string? CellPhoneNumber { get; }



		bool PaymentFeatureIsEnabled { get; }

		bool DepositeFeatureIsEnabled { get; }

		bool WithdrawFeatureIsEnabled { get; }

		bool TransferFeatureIsEnabled { get; }



		string Hash { get; }



		string? Description { get; }



		System.Collections.Generic.IReadOnlyList<ITransaction<T>> Transactions { get; }
	}
}
