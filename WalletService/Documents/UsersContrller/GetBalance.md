
# GetBalance

It allows the user to see the balance of his wallet

**URL** : `/api/users/getbalance`

**Method** : `POST`

**Auth required** : None

**Permissions required** : None

**Data examples**

```json
{
	"user": {
   "ip": "192.168.1.110",        
    "cellPhoneNumber": "o9391818607"
	},
	  "walletToken": "d630496e-3f91-4127-9dbc-f03b14ecd6d2", 
  "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}

```
## Success Responses

**Condition** : Data provided is valid.

**Code** : `200 OK`

**Content example** : 

```json
{
    "data": 10.00,
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
curl --location --request POST 'https://localhost:7087/api/users/getbalance' \
--header 'Content-Type: application/json' \
--header 'Accept: text/plain' \
--data-raw '{
	"user": {
   "ip": "192.168.1.110",        
    "cellPhoneNumber": "o939181860"
	},
	  "walletToken": "d630496e-3f91-4127-9dbc-f03b14ecd6d2", 
  "companyToken": "D24295E9-DAC0-4FE3-957F-6674F9FD0728"
}
'
```
