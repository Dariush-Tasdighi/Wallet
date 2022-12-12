
# Deposit

Allow the user to do withdraw transaction

**URL** : `/api/users/withdraw`

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
    "amount": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
    "userDescription": null,
    "systemicDescription": null,
    "additionalData": null
}
```

## Success Responses

**Condition** : Data provided is valid.

**Code** : `200 OK`

**Content example** : 

```json
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
```

## Error Response

**Condition** : If provided data is invalid, e.g. The field `Amount` must be between `0` and `500000000`..

**Code** : `400 BAD REQUEST`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-a3e0367d32d5b6858d0dd421260c2c32-d9b052f78e598b24-00",
    "errors": {
        "Amount": [
            "The field Amount must be between 0 and 500000000."
        ]
    }
}
```

## CURL

```curl
curl --location --request POST 'https://localhost:7087/api/users/withdraw' \
--header 'Content-Type: application/json' \
--data-raw '{
    "user": {
		"ip": "192.168.1.110",
		"cellPhoneNumber": "09123456789"
	},
    "amount": 1,
	"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
	"companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
    "userDescription": null,
    "systemicDescription": null,
    "additionalData": null
}'
```
