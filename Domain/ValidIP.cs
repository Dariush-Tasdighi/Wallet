﻿namespace Domain;

public class ValidIP : Seedwork.Entity, Dtat.Wallet.Abstractions.IValidIP<long>
{
	#region Constructor
	public ValidIP(long walletId, string serverIP) : base()
	{
		ServerIP = serverIP;
		WalletId = walletId;
		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	public long WalletId { get; private set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Wallet? Wallet { get; private set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
	public string ServerIP { get; set; }

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
	public string? Description { get; set; }

	public int RequestCount { get; set; }

	public System.DateTime? LastRequestDateTime { get; set; }

	public System.DateTime UpdateDateTime { get; private set; }

	#endregion /Properties
}