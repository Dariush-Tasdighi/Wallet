
# Deposit

Allow the user to get a transaction report

**URL** : `/api/users/gettransaction`

**Method** : `POST`

**Auth required** : None

**Permissions required** : None

**Data examples**

```json
{
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
	"transactionId": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}
```
## Success Responses

**Condition** : Data provided is valid.

**Code** : `200 OK`

**Content example** : 

```json
{
	"data": {
		"userId": 1,
		"walletId": 201,
		"amount": 500000.00,
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
```

## Error Response

**Condition** : If provided data is null or whitespace, e.g. The field `CellPhoneNumber` The CellPhoneNumber field is required...

**Code** : `400 BAD REQUEST`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-d8295bc299b549bc2dcb896682bb4b89-61f91e1541f43149-00",
    "errors": {
        "User.CellPhoneNumber": [
            "The CellPhoneNumber field is required."
        ]
    }
}
```

## CURL

```curl
curl --location --request POST 'https://localhost:7087/api/users/gettransaction' \
--header 'Content-Type: application/json' \
--data-raw '{
	"pageSize": 100000,
	"pageIndex": 1,
	"user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
    "transactionId": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}'
```
