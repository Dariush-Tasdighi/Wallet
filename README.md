# Hasti Wallet
Hasti Wallet is a simple **multi-company**, **multi-wallet** **multi-user** wallet with multiple features such as **payments**, **deposits**, **withdrawals** and **transfers**.

## Description

**Hasti Wallet** is a secure wallet platform. This project was created by **Dariush Tasdighi**,  and is sponsored by **Hasti innovative trader** inc.

- https://hasti.co/
- https://www.linkedin.com/company/hasti-innovative-trading/mycompany/

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
* Create a user by using following pay:

```yaml
{
    "user": {
        "ip": "192.168.1.110",
        "displayName": "داریوش تصدیقی",
        "cellPhoneNumber": "09121087461",
        "emailAddress": "DariushT@gmail.com",
        "nationalCode": "1234512345",
        "additionalData": null,
        "paymentFeatureIsEnabled": false,
        "depositeFeatureIsEnabled": true,
        "withdrawFeatureIsEnabled": false
    },
    "amount": 1000,
    "waletToken": "7c92aec0-a04f-475b-971b-b2a6655d18da",
    "providerName": "Saman Bank",
    "referenceCode": "1020304050",
    "userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
    "systemicDescription": "شارژ کیف پول",
    "additionalData": null
}
```

## Contributors 

- [Mr. Dariush Tasdighi](https://www.linkedin.com/in/Tasdighi/)
- [Mr. Mehran Rivadeh](https://www.linkedin.com/in/Mehran-Rivadeh-ab55845)
- [Mr. Ali Bayat](https://www.linkedin.com/in/AliBayatgh)

## Version History

* 2
    * Various bug fixes and optimizations
    * See [commit change]() or See [release history]()
* 1
    * Initial Release
