# Payment

Allows the user to pay from her wallet

**URL** : `/api/users/payment`

**Method** : `POST`

**Auth required** : None

**Permissions required** : None

**Data examples**

```json
{
    "user": {
        "cellPhoneNumber": "o9391818607",
        "ip": "192.168.1.110"
    },
    "amount": 8,
    "walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
    "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
    "referenceCode": "2030405060",
    "userDescription": null,
    "systemicDescription": "خرید محصول",
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
        "balance": 2.00,
        "transactionId": 2
    },
    "isSuccess": true,
    "errorMessages": [],
    "successMessages": []
}
```

## Error Response

**Condition** : If provided data is invalid, e.g. The field `Amount` must be between `0` and `500000000`..

**Code** : `200 OK`

**Content example** :

```json
{
    "data": null,
    "isSuccess": false,
    "errorMessages": [
        "There is not any user with this cell phone number!"
    ],
    "successMessages": []
}
```

## CURL

```json
curl --location --request POST 'https://localhost:7087/api/users/payment' \
--header 'Content-Type: application/json' \
--header 'Accept: text/plain' \
--data-raw '{
    "user": {
        "cellPhoneNumber": "o9391818607",
        "ip": "192.168.1.110"
    },
    "amount": 8,
    "walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
    "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
    "referenceCode": "2030405060",
    "userDescription": null,
    "systemicDescription": "خرید محصول",
    "additionalData": null
}'
'
```