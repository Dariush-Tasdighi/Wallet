import { check } from 'k6';
import http from 'k6/http';

export const options = {
	vus: 1000,
	duration: '1m',
	thresholds: {
		'http_req_duration': ['p(99)<1000']
	}
};

export default () => {

	const url =
		'https://localhost:7087/api/users/deposite';

	const payload =
		JSON.stringify({
			"user": {
				"ip": "192.168.1.110",
				"displayName": "علی بیات",
				"cellPhoneNumber": "09123456789",
				"emailAddress": "alibayatgh@GMail.com",
				"nationalCode": "1234512345",
				"additionalData": null,
				"paymentFeatureIsEnabled": false,
				"depositeFeatureIsEnabled": true,
				"withdrawFeatureIsEnabled": false
			},
			"amount": 1000,
			"walletToken": "D630496E-3F91-4127-9DBC-F03B14ECD6D2",
			"providerName": "ایران کیش",
			"referenceCode": "1020304050",
			"userDescription": "دوست داشتم کیف پولم رو شارژ کنم",
			"systemicDescription": "شارژ کیف پول",
			"additionalData": null
		});

	const params = {
		headers: {
			'Content-Type': 'application/json',
		},
	};

	const res =
		http.post(url, payload, params);

	check(res, {
		'is status 200': (r) => r.status === 200,
	});
};
