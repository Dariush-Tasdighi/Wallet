namespace Dtat.Wallet.Abstractions
{
	public interface ITransaction<T> : IBaseEntity<T>
	{
		T UserId { get; }

		T WalletId { get; }



		/// <summary>
		/// این فیلد (صرفا) می‌تواند مثبت یا منفی باشد
		/// و طبیعتا بی‌معنا خواهد بود که مقدار آن صفر شود
		/// در صورتی که مثبت باشد، مانده کیف پول شخص، افزایش می‌یابد
		/// و در صورتی که منفی باشد، مانده کیف پول شخص، کاهش می‌یابد
		/// </summary>
		decimal Amount { get; }

		double TimeDurationInMillisecond { get; }



		string? PaymentReferenceCode { get; }

		string? DepositeOrWithdrawProviderName { get; }

		string? DepositeOrWithdrawReferenceCode { get; }



		string? Hash { get; }

		string UserIP { get; }

		string ServerIP { get; }

		string? AdditionalData { get; }

		string? UserDescription { get; }

		string? SystemicDescription { get; }
	}
}
