namespace Domain;

public class Transaction :
	Seedwork.Entity, Dtat.Wallet.Abstractions.ITransaction<long>
{
	#region Constructor
	public Transaction(long userId, long walletId,
		decimal amount, string serverIP, string userIP) : base()
	{
		Amount = amount;
		UserId = userId;
		UserIP = userIP;
		ServerIP = serverIP;
		WalletId = walletId;
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



	#region PartyUserId
	public long? PartyUserId { get; set; }
	#endregion /PartyUserId

	#region PartyUser
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual User? PartyUser { get; private set; }
	#endregion /PartyUser



	#region Amount
	public decimal Amount { get; private set; }
	#endregion /Amount

	#region IsCleared
	/// <summary>
	/// تسویه شده
	/// </summary>
	public bool IsCleared { get; set; }
	#endregion /IsCleared

	#region Type
	public Dtat.Wallet.Abstractions.SeedWork.TransactionType Type { get; set; }
	#endregion /Type



	#region WithdrawDateTime
	public System.DateTime? WithdrawDateTime { get; set; }
	#endregion /WithdrawDateTime

	#region TransactionDuration
	public System.TimeSpan TransactionDuration { get; set; }
	#endregion /TransactionDuration



	#region PaymentReferenceCode
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.ReferenceCode)]
	public string? PaymentReferenceCode { get; set; }
	#endregion /PaymentReferenceCode

	#region DepositeOrWithdrawProviderName
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.ProviderName)]
	public string? DepositeOrWithdrawProviderName { get; set; }
	#endregion /DepositeOrWithdrawProviderName

	#region DepositeOrWithdrawReferenceCode
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.ReferenceCode)]
	public string? DepositeOrWithdrawReferenceCode { get; set; }
	#endregion /DepositeOrWithdrawReferenceCode



	#region Hash
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Hash)]
	public string? Hash { get; private set; }
	#endregion /Hash

	#region UserIP
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string UserIP { get; set; }
	#endregion /UserIP

	#region ServerIP
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.IP)]
	public string ServerIP { get; set; }
	#endregion /ServerIP

	#region AdditionalData
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }
	#endregion /AdditionalData

	#region UserDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? UserDescription { get; set; }
	#endregion /UserDescription

	#region SystemicDescription
	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.SeedWork.Constant.MaxLength.Description)]
	public string? SystemicDescription { get; set; }
	#endregion /SystemicDescription

	#endregion /Properties

	#region Methods

	public string GetHash()
	{
		var stringBuilder =
			new System.Text.StringBuilder();

		stringBuilder.Append
			(value: $"{nameof(InsertDateTime)}:{Dtat.ConvertForHashing.FromDateTime(value: InsertDateTime)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UserId)}:{UserId}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(WalletId)}:{WalletId}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(PartyUserId)}:{PartyUserId}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(Amount)}:{Dtat.ConvertForHashing.FromDecimal(value: Amount)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(IsCleared)}:{IsCleared}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(WithdrawDateTime)}:{Dtat.ConvertForHashing.FromDateTime(value: WithdrawDateTime)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(TransactionDuration)}:{TransactionDuration}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(PaymentReferenceCode)}:{Dtat.ConvertForHashing.FromString(value: PaymentReferenceCode)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(DepositeOrWithdrawProviderName)}:{Dtat.ConvertForHashing.FromString(value: DepositeOrWithdrawProviderName)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(DepositeOrWithdrawReferenceCode)}:{Dtat.ConvertForHashing.FromString(value: DepositeOrWithdrawReferenceCode)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UserIP)}:{Dtat.ConvertForHashing.FromString(value: UserIP)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(ServerIP)}:{Dtat.ConvertForHashing.FromString(value: ServerIP)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(AdditionalData)}:{Dtat.ConvertForHashing.FromString(value: AdditionalData)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(UserDescription)}:{Dtat.ConvertForHashing.FromString(value: UserDescription)}");

		stringBuilder.Append
			(value: Dtat.ConvertForHashing.Separator());

		stringBuilder.Append
			(value: $"{nameof(SystemicDescription)}:{Dtat.ConvertForHashing.FromString(value: SystemicDescription)}");

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

	#endregion /Methods
}
