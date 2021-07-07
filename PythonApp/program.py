#Test site reference: test_icmarketseultd89904
#username: jwt@icmarkets.com
#secret: 60-5822a14eaab668c197f7b55d519af46c74fdd54f6dcdba44d510fdf66aa4dc85

# !/usr/bin/python
import securetrading

print('starting...')

stconfig = securetrading.Config()
stconfig.username = "ws@icmarkets.com"
stconfig.password = "J,4b(j!t"
st = securetrading.Api(stconfig)

request = {
    "sitereference": "test_icmarketseultd89904",
    "requesttypedescriptions": ["AUTH"],
    "accounttypedescription": "ECOM",
    "currencyiso3a": "USD",
    "baseamount": "2000",
    "orderreference": "12345678",
    "billingfirstname": "Fname",
    "billinglastname": "Lname",
    "pan": "4111111111111111",
    "expirydate": "12/2022",
    "securitycode": "123"
}

strequest = securetrading.Request()
strequest.update(request)
stresponse = st.process(strequest)  # stresponse contains the transaction response

print('done')
print(stresponse)
