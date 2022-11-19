using Domain;

namespace Tests;

public class UserWalletTest
{
    [Fact]
    public async Task Concurrency_Test()
    {
        decimal expectedBalance = 10_000;
        UserWallet userWallet = new(userId: 1, walletId: 1);
        Task[] tasks = new Task[1000];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                decimal[] amounts = { 8, 2, -2, 6, -6, -3, 0, -5, 11, -1 };
                foreach (var amount in amounts)
                {
                    if (amount >= 0)
                    {
                        userWallet.Deposit(amount);
                    }
                    else
                    {
                        userWallet.Withdraw(Math.Abs(amount));
                    }
                }
            }
            );
        }
        await Task.WhenAll(tasks);

        var actualBalance = userWallet.GetBalance();

        Assert.Equal(expectedBalance, actualBalance);
    }
}