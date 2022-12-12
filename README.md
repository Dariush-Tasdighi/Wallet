# Hasti Wallet
Hasti Wallet is a simple **multi-company**, **multi-wallet** **multi-user** wallet with multiple features such as **payments**, **deposits**, **withdrawals** and **transfers**.

## Description

**Hasti Wallet** is a secure wallet platform. This project was created by **Dariush Tasdighi**,  and is sponsored by **Hasti innovative trader** inc.

- https://hasti.co/
- https://www.linkedin.com/company/hasti-innovative-trading/mycompany/

## Features

- Refund
- Transfer
- Payment
- Deposite
- Withdraw

## Getting Started
Make sure you have installed **.NET 7** in your environment. After that, you can run the below commands from the **/WalletService/** directory and get started with the `Hasti wallet` immediately.

    dotnet run
    
### Dependencies

* .NET 7
* SQL Server

### Executing program

* Download & Install **.NET 7** from [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Configure connection strings
* Run the project
* Create or update an user by using following json in deposite method (POST):

```yaml
{
        "user": {
                "ip": "192.168.1.110",
                "displayName": "Ali Reza Alavi",
                "cellPhoneNumber": "09123456789",
                "emailAddress": "AliReza@GMail.com",
                "nationalCode": "1234512345",
                "additionalData": null,
                "paymentFeatureIsEnabled": false,
                "depositeFeatureIsEnabled": true,
                "withdrawFeatureIsEnabled": false
        },
        "amount": 1,
        "walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
        "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
        "providerName": "Iran Kish",
        "referenceCode": "1020304050",
        "userDescription": "Something...",
        "systemicDescription": "Wallet Charged",
        "additionalData": null
}
```

## Contributors (LinkedIn)

- [Mr. Dariush Tasdighi](https://www.linkedin.com/in/Tasdighi/)
- [Mr. Ali Khoshandam Esfahani](https://www.linkedin.com/in/ali-khoshandam-esfahani-55720767/)
- [Mr. Mehran Rivadeh](https://www.linkedin.com/in/Mehran-Rivadeh-ab55845)
- [Mr. Reza Qadimi](https://www.linkedin.com/in/Reza-Qadimi)
- [Mr. Ali Bayat](https://www.linkedin.com/in/AliBayatgh)
