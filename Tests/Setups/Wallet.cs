namespace Tests.Setups;

internal class Wallet : object
{
	internal class Hit : Base.WalletBase
	{
		internal static Hit Instance
		{
			get
			{
				return new Hit();
			}
		}

		private Hit() : base()
		{
			IP = "192.168.1.110";

			Token = System.Guid.NewGuid();

			Wallet = new(name: "کیف پول هستی")
			{
				IsActive = true,
			};
		}
	}
}
