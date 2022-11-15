namespace Domain;

public class Transaction : Seedwork.Entity, Dtat.Wallet.Abstractions.ITransaction<long>
{
	#region Constructor
	public Transaction(long userId, long walletId,
		decimal amount, string userIP, string serverIP) : base()
	{
		Amount = amount;
		UserId = userId;
		UserIP = userIP;
		ServerIP = serverIP;
		WalletId = walletId;
	}
	#endregion /Constructor

	#region Properties

	public long UserId { get; private set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual User? User { get; private set; }



	public long WalletId { get; private set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }



	public decimal Amount { get; private set; }

	public double TimeDurationInMillisecond { get; set; }



	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ReferenceCode)]
	public string? PaymentReferenceCode { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ProviderName)]
	public string? DepositeOrWithdrawProviderName { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ReferenceCode)]
	public string? DepositeOrWithdrawReferenceCode { get; set; }



	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Hash)]
	public string? Hash { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
	public string UserIP { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
	public string ServerIP { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
	public string? AdditionalData { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? UserDescription { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? SystemicDescription { get; set; }
	#endregion /Properties
}
