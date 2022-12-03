public class UserWalletTest : object
{
	public UserWalletTest() : base()
	{
	}

	[Xunit.Fact]
	public async System.Threading.Tasks.Task Concurrency_Test()
	{
		decimal expectedBalance = 10_000;

		var userWallet =
			new Domain.UserWallet(userId: 1, walletId: 1);

		var tasks =
			new System.Threading.Tasks.Task[1000];

		for (int index = 0; index < tasks.Length; index++)
		{
			tasks[index] = System.Threading.Tasks.Task.Run(() =>
			{
				decimal[] amounts =
					{ 8, 2, -2, 6, -6, -3, 0, -5, 11, -1 };

				foreach (var amount in amounts)
				{
					if (amount >= 0)
					{
						userWallet.Balance += amount;
					}
					else
					{
						userWallet.Balance -= System.Math.Abs(amount);
					}
				}
			});
		}

		await System.Threading.Tasks.Task.WhenAll(tasks);

		var actualBalance =
			userWallet.Balance;

		Xunit.Assert.Equal(expectedBalance, actualBalance);
	}
}
