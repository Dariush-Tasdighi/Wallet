namespace Tests.Builders.Models;

internal class WalletBuilder : object
{
	internal static WalletBuilder Create()
	{
		var newWallet =
			new WalletBuilder();

		return newWallet;
	}

	private WalletBuilder() : base()
	{
		IsActive = true;

		RefundFeatureIsEnabled = false;

		PaymentFeatureIsEnabled = false;

		DepositeFeatureIsEnabled = false;

		TransferFeatureIsEnabled = false;

		WithdrawFeatureIsEnabled = false;

		Name = Helpers.Constants.Shared.Wallet.Hit;
	}

	protected string Name { get; private set; }

	protected bool IsActive { get; private set; }

	protected string? Description { get; private set; }

	protected string? AdditionalData { get; private set; }

	protected bool DepositeFeatureIsEnabled { get; private set; }

	protected bool PaymentFeatureIsEnabled { get; private set; }

	protected bool RefundFeatureIsEnabled { get; private set; }

	protected bool TransferFeatureIsEnabled { get; private set; }

	protected bool WithdrawFeatureIsEnabled { get; private set; }

	internal WalletBuilder Named(string name)
	{
		Name = name;

		return this;
	}

	internal WalletBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	internal WalletBuilder WithAdditionalData(string? additionalData)
	{
		AdditionalData = additionalData;

		return this;
	}

	internal WalletBuilder ThatDepositeFeatureIsEnabled(bool isEnabled = true)
	{
		DepositeFeatureIsEnabled = isEnabled;

		return this;
	}

	internal WalletBuilder ThatPaymentFeatureIsEnabled(bool isEnabled = true)
	{
		PaymentFeatureIsEnabled = isEnabled;

		return this;
	}

	internal WalletBuilder ThatRefundFeatureIsEnabled(bool isEnabled = true)
	{
		RefundFeatureIsEnabled = isEnabled;

		return this;
	}

	internal WalletBuilder ThatWithdrawFeatureIsEnabled(bool isEnabled = true)
	{
		WithdrawFeatureIsEnabled = isEnabled;

		return this;
	}

	internal WalletBuilder ThatTransferFeatureIsEnabled(bool isEnabled = true)
	{
		TransferFeatureIsEnabled = isEnabled;

		return this;
	}

	internal WalletBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	internal Domain.Wallet Build()
	{
		var newWallet =
			new Domain.Wallet(name: Name)
			{
				IsActive = IsActive,
				Description = Description,
				AdditionalData = AdditionalData,
				RefundFeatureIsEnabled = RefundFeatureIsEnabled,
				PaymentFeatureIsEnabled = PaymentFeatureIsEnabled,
				DepositeFeatureIsEnabled = DepositeFeatureIsEnabled,
				TransferFeatureIsEnabled = TransferFeatureIsEnabled,
				WithdrawFeatureIsEnabled = WithdrawFeatureIsEnabled,
			};

		return newWallet;
	}
}
