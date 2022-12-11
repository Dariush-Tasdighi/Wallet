
# Deposit

Allow the user to increase their wallet balance

**URL** : `/api/users/deposite`

**Method** : `POST`

**Auth required** : None

**Permissions required** : None

**Data constraints**

```json
{
    "amount": "[1 to 1000_000]",
}
```

**Data examples**

```json
{
    "user": {
    "ip": "192.168.1.110",        
    "cellPhoneNumber": "o9391818607",
    "displayName": "داریوش تصدیقی",
    "emailAddress": "ali.bayat.gh@gmail.com",
    "nationalCode": "3309891988",
    "additionalData": null,
    "paymentFeatureIsEnabled": true,
    "depositeFeatureIsEnabled": true,
    "withdrawFeatureIsEnabled": false,
    "transferFeatureIsEnabled": true
  },
  "amount": 10,
  "walletToken": "d630496e-3f91-4127-9dbc-f03b14ecd6d2", 
  "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
  "providerName": "ایران کیش",
  "referenceCode": "1020304050",
  "userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
  "systemicDescription": "شارژ کیف پول",
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
        "balance": 10,
        "transactionId": 1
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
curl --location --request POST 'https://localhost:7087/api/users/deposite' \
--header 'Content-Type: application/json' \
--header 'Accept: text/plain' \
--data-raw '{
    "user": {
    "ip": "192.168.1.110",        
    "cellPhoneNumber": "o9391818607",
    "displayName": "داریوش تصدیقی",
    "emailAddress": "ali.bayat.gh@gmail.com",
    "nationalCode": "3309891988",
    "additionalData": null,
    "paymentFeatureIsEnabled": true,
    "depositeFeatureIsEnabled": true,
    "withdrawFeatureIsEnabled": false,
    "transferFeatureIsEnabled": true
  },
  "amount": 10,
  "walletToken": "d630496e-3f91-4127-9dbc-f03b14ecd6d2", 
  "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728",
  "providerName": "ایران کیش",
  "referenceCode": "1020304050",
  "userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
  "systemicDescription": "شارژ کیف پول",
  "additionalData": null
}'
```
