**************************************************
public static System.Guid Wallet =
	new(g: "D630496E-3F91-4127-9DBC-F03B14ECD6D2");

public static System.Guid Company =
	new(g: "D24295E9-DAC0-4FE3-957F-6674F9FD0728");
**************************************************

**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"displayName": "علی رضا علوی",
		"cellPhoneNumber": "09123456789",
		"emailAddress": "AliReza@GMail.com",
		"nationalCode": "1234512345",
		"additionalData": null,
		"paymentFeatureIsEnabled": true,
		"depositeFeatureIsEnabled": true,
		"withdrawFeatureIsEnabled": false
	},
	"amount": 1,
	"waletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
	"providerName": "ایران کیش",
	"referenceCode": "1020304050",
	"userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
	"systemicDescription": "شارژ کیف پول",
	"additionalData": null
}
**************************************************

**************************************************
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"amount": 1,
	"waletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
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
https://localhost:7087/api/users/GetBalance/D630496E-3F91-4127-9DBC-F03B14ECD6D2/09121087461
**************************************************

**************************************************
//[Microsoft.AspNetCore.Mvc.Consumes
//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

//[Microsoft.AspNetCore.Mvc.Produces
//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]
**************************************************
