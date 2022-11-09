namespace Dtat.Wallet.Abstractions
{
	public interface IApplication<T>
	{
		T Id { get; set; }

		string Name { get; set; }

		string Token { get; set; }

		string ValidIPs { get; set; }



		bool PaymentFeatureIsEnabled { get; set; }

		bool DepositeFeatureIsEnabled { get; set; }

		bool WithdrawFeatureIsEnabled { get; set; }

		bool TransferFeatureIsEnabled { get; set; }
	}
}
