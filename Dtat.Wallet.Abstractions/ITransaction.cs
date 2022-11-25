namespace Dtat.Wallet.Abstractions;

public interface ITransaction<T> :
	SeedWork.IEntity<T>, SeedWork.IHashing<T> where T : struct
{
	T UserId { get; }

	T WalletId { get; }

	/// <summary>
	/// طرف حساب
	/// </summary>
	T? PartyUserId { get; }



	/// <summary>
	/// این فیلد (صرفا) می‌تواند مثبت یا منفی باشد
	/// و طبیعتا بی‌معنا خواهد بود که مقدار آن صفر باشد
	/// در صورتی که مقدار آن مثبت باشد، مانده کیف پول شخص، افزایش می‌یابد
	/// و در صورتی که مقدار آن منفی باشد، مانده کیف پول شخص، کاهش می‌یابد
	/// </summary>
	decimal Amount { get; }

	System.TimeSpan TransactionDuration { get; }



	string UserIP { get; }

	string ServerIP { get; }



	string? AdditionalData { get; }

	string? UserDescription { get; }

	string? SystemicDescription { get; }



	string? PaymentReferenceCode { get; }

	string? DepositeOrWithdrawProviderName { get; }

	string? DepositeOrWithdrawReferenceCode { get; }
}
