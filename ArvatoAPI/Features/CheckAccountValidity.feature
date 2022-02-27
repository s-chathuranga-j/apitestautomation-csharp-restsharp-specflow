Feature: CheckAccountValidity
	Checks the validity of a given Account Number

Scenario: Verify with valid Bank account number
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount GB09HAOE91311808002317
	Then the response returns with responseCode 200
	And isValid as True
	
Scenario: Verify Bank account number length
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount <accountNumber>
	Then the response returns with responseCode <responseCode>
	And isValid as <isValid>
	
	Examples: 
	| accountNumber                           | responseCode | isValid | responseMessage                                   | test scenario        |
	| GB09HA                                  | 400          | null    | A string value with minimum length 7 is required. | char length < 7      |
	| GB09HAO                                 | 200          | False   | null                                              | char length = 7      |
	| GB09HAOE9131180                         | 200          | False   | null                                              | 7 < char length < 34 |
	| GB09HAOE91311808002317311808002317      | 200          | False   | null                                              | char length = 34     |
	| GB09HAOE91311808002317311808002317E4567 | 400          | null    | A string value with minimum length 7 is required. | char length > 34     |
 
Scenario: Verify with Bank account format
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount <accountNumber>
	Then the response returns with responseCode <responseCode>
	And isValid as <isValid>
	
	Examples: 
	| accountNumber                  | responseCode | isValid | responseMessage                                   | test scenario                                                  |
	| AL4721211ALBNCD000000235698741 | 200          | False   | Value format is incorrect.                        | invalid account number (contains more than 4 characters values |
	| &L4721211AL90000000235698741   | 200          | False   | Value format is incorrect.                        | account number with special char                               |
	|                                | 400          | null    | Value is required.                                | empty account number                                           |
	| null                           | 400          | null    | A string value with minimum length 7 is required. | null account number                                            |
 
Scenario: Verify with Bank accounts from different countires
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount <accountNumber>
	Then the response returns with responseCode <responseCode>
	And isValid as <isValid>
 
	Examples: 
	  | accountNumber                      | responseCode | isValid | IBANCountry | test scenario              |
	  | NO 93 8601 1117947                 | 200          | True    | Norway      | norway IBAN                |
	  | DE89 3704 0044 0532 0130 00        | 200          | True    | Germany     | germany IBAN               |
	  | AT61 1904 3002 3457 3201           | 200          | True    | Austria     | austria IBAN               |
	  | CH93 0076 2011 6238 5295 7         | 200          | True    | Switzerland | switzerland IBAN           |
	  | FI21 1234 5600 0007 85             | 200          | True    | Finland     | finland IBAN               |
	  | SE45 5000 0000 0583 9825 7466      | 200          | True    | Sweden      | sweden IBAN                |
	  | DK50 0040 0440 1162 43             | 200          | True    | Denmark     | denmark IBAN               |
	  | AL47 2121 1009 0000 0002 3569 8741 | 200          | False   | Albania     | non-supported country IBAN |
	  | EE382200221020145685               | 200          | False   | Estonia     | non-supported country IBAN |
   
Scenario: POST request content type validations
	Given the contentType is <contentType>
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount GB09HAOE91311808002317
	Then the response returns with responseCode <responseCode>
	
	Examples: 
	  | contentType      | responseCode | test scenario                 |
	  | application/json | 200          | sent as JSON content          |
	  | text/html        | 415          | sent as HTML content          |
   
Scenario: POST request authorization key validations
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is <authKey>
	When the request posted with bankAccount GB09HAOE91311808002317
	Then the response returns with responseCode <responseCode>
	And the response message is <responseMessage>
	
	Examples:
	| authKey                                  | responseCode | responseMessage                                 | test scenario    |
	| Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L | 200          | null                                            | valid auth key   |
	| Q7DaxRnFls6IpwSW1SQ2FaTFO                | 401          | Authorization has been denied for this request. | invalid auth key |
	| null                                     | 401          | Authorization has been denied for this request. | empty auth key   |
 
Scenario: POST request authorization type validations
	Given the contentType is application/json
	And the authType is <authType>
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount GB09HAOE91311808002317
	Then the response returns with responseCode <responseCode>
	
	Examples: 
	| authType     | responseCode | responseMessage                                 | test scenario     |
	| X-Auth-Key   | 200          | null                                            | valid auth type   |
	| X-Auth_Email | 401          | Authorization has been denied for this request. | invalid auth type |
	| null         | 401          | Authorization has been denied for this request. | empty auth type   |
 
Scenario: POST request without request body
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request is posted
	Then the response returns with responseCode 400
	And the response message is The body of the request is missing, or its format is invalid.
	
Scenario: POST request without request headers
	Given the contentType is application/json
	When the request is posted without headers and bankAccount GB09HAOE91311808002317
	Then the response returns with responseCode 401
	And the response message is Authorization has been denied for this request.
	
Scenario: Verify response data of invalid bank account number request
	Given the contentType is application/json
	And the authType is X-Auth-Key
	And the authKey is Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L
	When the request posted with bankAccount AL4721211AL90000000235698741
	Then the response returns with responseCode 200
	And isValid as False
	And the response type is BusinessError
	And the response code contains 200
	And the response message is Value format is incorrect.
	And the response actionCode is AskConsumerToReEnterData
	And the response fieldReference is bankAccount