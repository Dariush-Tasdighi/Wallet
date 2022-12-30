namespace Tests.Builders.Models;

public class WalletBuilder : object
{
	public static WalletBuilder Create()
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

		Name =
			Setups.Constants.Shared.Wallet.Hit;
	}

	public string Name { get; private set; }

	public bool IsActive { get; private set; }

	public string? Description { get; private set; }

	public string? AdditionalData { get; private set; }

	public bool DepositeFeatureIsEnabled { get; private set; }

	public bool PaymentFeatureIsEnabled { get; private set; }

	public bool RefundFeatureIsEnabled { get; private set; }

	public bool TransferFeatureIsEnabled { get; private set; }

	public bool WithdrawFeatureIsEnabled { get; private set; }

	public WalletBuilder Named(string name)
	{
		Name = name;

		return this;
	}

	public WalletBuilder WithDescription(string? description)
	{
		Description = description;

		return this;
	}

	public WalletBuilder WithAdditionalData(string? additionalData)
	{
		AdditionalData = additionalData;

		return this;
	}

	public WalletBuilder ThatDepositeFeatureIsEnabled(bool isEnabled = true)
	{
		DepositeFeatureIsEnabled = isEnabled;

		return this;
	}

	public WalletBuilder ThatPaymentFeatureIsEnabled(bool isEnabled = true)
	{
		PaymentFeatureIsEnabled = isEnabled;

		return this;
	}

	public WalletBuilder ThatRefundFeatureIsEnabled(bool isEnabled = true)
	{
		RefundFeatureIsEnabled = isEnabled;

		return this;
	}

	public WalletBuilder ThatWithdrawFeatureIsEnabled(bool isEnabled = true)
	{
		WithdrawFeatureIsEnabled = isEnabled;

		return this;
	}

	public WalletBuilder ThatTransferFeatureIsEnabled(bool isEnabled = true)
	{
		TransferFeatureIsEnabled = isEnabled;

		return this;
	}

	public WalletBuilder ThatIsActive(bool isActive = true)
	{
		IsActive = isActive;

		return this;
	}

	public Domain.Wallet Build()
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
