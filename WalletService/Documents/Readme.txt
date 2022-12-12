**************************************************
public static System.Guid Wallet =
	new(g: "D630496E-3F91-4127-9DBC-F03B14ECD6D2");

public static System.Guid Company =
	new(g: "D24295E9-DAC0-4FE3-957F-6674F9FD0728");
**************************************************

**************************************************
*** Deposite *************************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"displayName": "داریوش تصدیقی",
		"cellPhoneNumber": "09121087461",
		"emailAddress": "DariushT@GMail.com",
		"nationalCode": "1234512345",
		"additionalData": null
	},
	"amount": 10,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
	"providerName": "ایران کیش",
	"referenceCode": "1020304050",
	"userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
	"systemicDescription": "شارژ کیف پول",
	"additionalData": null
}
**************************************************

**************************************************
*** GetBalance ***********************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09121087461"
	},
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}
**************************************************

**************************************************
*** Payment **************************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09121087461"
	},
	"amount": 8,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
	"referenceCode": "2030405060",
	"userDescription": null,
	"systemicDescription": "خرید محصول",
	"additionalData": null
}
**************************************************

**************************************************
{
	"data": null,
	"isSuccess": false,
	"errorMessages": [
		"Payment feature is not enabled for this user!"
	],
	"successMessages": []
}
**************************************************

**************************************************
{
	"data": {
		"balance": 1,
		"transactionId": 3
	},
	"isSuccess": true,
	"errorMessages": [],
	"successMessages": []
}
**************************************************

**************************************************
*** Refund ***************************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"amount": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
	"withdrawDurationInDays": null,
	"transactionId": 1,
	"userDescription": null,
	"systemicDescription": null,
	"additionalData": null
}
**************************************************

**************************************************
{
	"data": null,
	"isSuccess": false,
	"errorMessages": [
		"Refund feature is not enabled for this user!"
	],
	"successMessages": []
}
**************************************************

**************************************************
{
	"data": {
		"balance": 100,
		"transactionId": 4,
		"withdrawBalance": 10
	},
	"isSuccess": true,
	"errorMessages": [],
	"successMessages": []
}
**************************************************

**************************************************
*** GetTransaction *******************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"transactionId": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}
**************************************************

**************************************************
{
	"data": {
		"userId": 1,
		"walletId": 201,
		"amount": 500000,
		"isCleared": false,
		"type": 1,
		"withdrawDate": null,
		"insertDateTime": "2022-12-11T18:35:19.4659476",
		"paymentReferenceCode": null,
		"depositeOrWithdrawProviderName": "ایران کیش",
		"depositeOrWithdrawReferenceCode": "1020304050",
		"userIP": "192.168.1.110",
		"additionalData": null,
		"userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
		"systemicDescription": "شارژ کیف پول"
	},
	"isSuccess": true,
	"errorMessages": [],
	"successMessages": []
}
**************************************************

**************************************************
*** GetTransactions ******************************
**************************************************
{
	"pageSize": 10,
	"pageIndex": 1,
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}
**************************************************

**************************************************
{
	"data": {
		"totalCount": 14,
		"depositeTotalAmount": 4500001,
		"withdrawTotalAmount": -20000,
		"depositeCurrentItemsTotalAmount": 500000,
		"withdrawCurrentItemsTotalAmount": 0,
		"items": [
		{
			"userId": 1,
			"walletId": 201,
			"amount": 500000,
			"isCleared": false,
			"type": 1,
			"withdrawDate": null,
			"insertDateTime": "2022-12-11T18:35:19.4659476",
			"paymentReferenceCode": null,
			"depositeOrWithdrawProviderName": "ایران کیش",
			"depositeOrWithdrawReferenceCode": "1020304050",
			"userIP": "192.168.1.110",
			"additionalData": null,
			"userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
			"systemicDescription": "شارژ کیف پول"
		}]
	},
	"isSuccess": true,
	"errorMessages": [],
	"successMessages": []
}
**************************************************

**************************************************
*** Withdraw *************************************
**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"amount": 100,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
	"userDescription": null,
	"systemicDescription": null,
	"additionalData": null
}

**************************************************

**************************************************
{
	"data": {
		"transactionId": 2,
		"balance": 100000.00,
		"withdrawBalance": 0.00
	},
	"isSuccess": true,
	"errorMessages": [],
	"successMessages": []
}
**************************************************

**************************************************
*** Refund ***************************************
**************************************************
{
    "user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
    "amount": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
    "withdrawDurationInDays": 0,
    "systemicDescription": null,
    "additionalData": null
}
**************************************************

**************************************************
{
    "data": {
        "transactionId": 2,
        "balance": 100000.00,
        "withdrawBalance": 0.00
    },
    "isSuccess": true,
    "errorMessages": [],
    "successMessages": []
}
**************************************************

**************************************************
https://localhost:7087/api/users/GetBalance/D630496E-3F91-4127-9DBC-F03B14ECD6D2/09121087461
**************************************************

**************************************************
//[Microsoft.AspNetCore.Mvc.Consumes
//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

//[Microsoft.AspNetCore.Mvc.Produces
//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]
**************************************************
