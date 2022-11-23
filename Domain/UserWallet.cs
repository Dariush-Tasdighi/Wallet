﻿using System;

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

	#region UserId
	public long UserId { get; private set; }
	#endregion /UserId

	#region User
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual User? User { get; private set; }
	#endregion /User



	#region WalletId
	public long WalletId { get; private set; }
	#endregion /WalletId

	#region Wallet
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }
	#endregion /Wallet



	#region IsActive
	public bool IsActive { get; set; }
	#endregion /IsActive

	#region UpdateDateTime
	public System.DateTime UpdateDateTime { get; private set; }
	#endregion /UpdateDateTime






	#region PaymentFeatureIsEnabled
	public bool PaymentFeatureIsEnabled { get; set; }
	#endregion /PaymentFeatureIsEnabled

	#region DepositeFeatureIsEnabled
	public bool DepositeFeatureIsEnabled { get; set; }
	#endregion /DepositeFeatureIsEnabled

	#region WithdrawFeatureIsEnabled
	public bool WithdrawFeatureIsEnabled { get; set; }
	#endregion /WithdrawFeatureIsEnabled

	/// <summary>
	/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
	/// </summary>
	//public bool TransferFeatureIsEnabled { get; set; }



	#region Hash
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Hash)]
	public string? Hash { get; private set; }
	#endregion /Hash

	#region Description
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? Description { get; set; }
	#endregion /Description

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#endregion /Properties

	#region Methods

	public string GetHash()
	{
		var stringBuilder =
			new System.Text.StringBuilder();

		stringBuilder.Append($"{nameof(InsertDateTime)}:{InsertDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(UserId)}:{UserId}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(WalletId)}:{WalletId}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(IsActive)}:{IsActive}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(UpdateDateTime)}:{UpdateDateTime}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(balance)}:{balance}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(PaymentFeatureIsEnabled)}:{PaymentFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(DepositeFeatureIsEnabled)}:{DepositeFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(WithdrawFeatureIsEnabled)}:{WithdrawFeatureIsEnabled}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(Description)}:{Description}");
		stringBuilder.Append('|');
		stringBuilder.Append($"{nameof(AdditionalData)}:{AdditionalData}");

		var text =
			stringBuilder.ToString();

		string result =
			Dtat.Utility.GetSha256(text: text);

		return result;
	}

	public void UpdateHash()
	{
		Hash = GetHash();
	}

	public bool CheckHashValidation()
	{
		var result = GetHash();

		if (string.Compare(result, Hash, ignoreCase: true) == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

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
    #endregion /Methods
}
