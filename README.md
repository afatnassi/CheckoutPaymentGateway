# CheckoutPaymentGateway
This repository contains two solutions:
* DummyBankApi: A bank simulator
* PaymentGateway: a gateway that allows a merchant to:
    - To process a payment through your payment gateway.
    - To retrieve details of a previously made payment.

## How to run the solution:

* Open the PaymentGateway solution in visual studio as <b>Admin</b> (to be able to write in the sqlite db) and set PaymentGateway.API and starting project.
* Open VS CLI and select PaymentGateway.Infrastructure Project in the dropdown and run this command to create your DB: 
  > update-database

![update database](https://user-images.githubusercontent.com/12180272/167817827-000ff8f7-2c5d-4a38-8834-44b2064d5435.png)

* Open the DummyBankApi solution in visual studio and run it ( select DummyBankApi and not IISExpress)

![Dummy bank](https://user-images.githubusercontent.com/12180272/167818709-38c2f1bf-3888-4590-b647-bf6d776f4cd1.png)

* Run the PaymentGatewayApi project ( select PaymentGatewayApi and not IISExpress)

![PaymentGateway](https://user-images.githubusercontent.com/12180272/167818853-6a283d18-b985-465e-b997-2d8d6351202e.png)

Use the Swagger UI interface to make api calls.

## Assumptions made:

* Just for this POC and to test it locally, the solution supports only two currencoes: EUR and USD and the Bank simulator will return failed response when currency is not EUR.
* I return status code 402 for failed Transaction/payment.
* The Bank Service can be unreachable/unavailable, that's why i create an entry of the transaction in our db in status processing before making the bankService call.

## Areas of improvements

* Use Polly to handle retry policies when contacting the external Bank service.
* Use MediatR to create request/handler or (publish/subscribe) Instead of directly calling the bank service, create a an bankProcessingRequest and handle it elsewhere. That way we can add/change handler without touching our PaymentFacade.
* Add reason code to response to explain why the transaction failed.
* Encrypt the credit card data stored on our DB
* Map the expiryDate field to a two field month and year in our DB
* Test the validation of requests
* Add Api tests/ performance tests.
* Better logging

## Cloud Technoligies to use:
* Use RabbitMq/ServiceBus to handle interaction between paymentGateway and bank api
* Use Azure KeyVolt to store sensitive information like creditcard details
* Use CosmosDB to store transaction status/id.
* Use azure devops CI/CD and Azure App Service to build/deploy and host the App 
