using System;

namespace Domain;

public class UserWallet : Seedwork.Entity, Dtat.Wallet.Abstractions.IUserWallet<long>
{
    private readonly object balanceLock = new object();
    private decimal balance;
    #region Constructor
    public UserWallet(long userId, long walletId) : base()
    {
        UserId = userId;
        WalletId = walletId;

        UpdateDateTime = InsertDateTime;
    }
    #endregion /Constructor

    #region Properties

    public long UserId { get; private set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual User? User { get; private set; }



    public long WalletId { get; private set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Wallet? Wallet { get; private set; }



    public bool IsActive { get; set; }

    public System.DateTime UpdateDateTime { get; private set; }

    public bool PaymentFeatureIsEnabled { get; set; }

    public bool DepositeFeatureIsEnabled { get; set; }

    public bool WithdrawFeatureIsEnabled { get; set; }

    /// <summary>
    /// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
    /// </summary>
    //public bool TransferFeatureIsEnabled { get; set; }



    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Dtat.Wallet.Abstractions.Constant.MaxLength.Hash)]
    public string? Hash { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
    public string? Description { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
    public string? AdditionalData { get; set; }
    #endregion /Properties

    #region Methods
    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "The deposit amount cannot be negative.");

        lock (balanceLock)
        {
            balance += amount;
        }
    }

    public decimal Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The withdraw amount cannot be negative.");
        }

        decimal appliedAmount = 0;
        lock (balanceLock)
        {
            if (balance >= amount)
            {
                balance -= amount;
                appliedAmount = amount;
            }
        }

        return appliedAmount;
    }

    public decimal GetBalance()
    {
        lock (balanceLock)
        {
            return balance;
        }
    }
    #endregion
}
