namespace Dtat.Wallet.Abstractions
{
	public interface IUser<T, P>
	{
		T Id { get; set; }

		string ApplicationUserId { get; set; }

		decimal Balance { get; set; }



		string? Username { get; set; }

		string? LastName { get; set; }

		string? FirstName { get; set; }

		string? EmailAddress { get; set; }

		string? CellPhoneNumber { get; set; }



		bool PaymentFeatureIsEnabled { get; set; }

		bool DepositeFeatureIsEnabled { get; set; }

		bool WithdrawFeatureIsEnabled { get; set; }

		bool TransferFeatureIsEnabled { get; set; }



		string Hash { get; set; }



		string? Description { get; set; }
	}
}
