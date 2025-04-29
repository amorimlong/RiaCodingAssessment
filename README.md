# RiaCodingAssessment
This repository aims to delivery Ria Codign Assessment.

This repistory contains two porposed coding challenges

## 1 - Denomination routine

A console appication that list of possible combinations which an ATM with three cartridges (10 EUR, 50 EUR and 100 EUR) can pay out the following values:

- 30 EUR
- 50 EUR
- 60 EUR
- 80 EUR
- 140 EUR
- 230 EUR
- 370 EUR
- 610 EUR
- 980 EUR

### 1.1 - Running the project

- Open command tool and access the directory CalculatePayout\src\CalculatePayout

- run the command ``` dotnet run ```

## 2 - Customers API

An API that manager Customer information. There is 2 endpoints 

- POST that allow creating more than one customer
- GET list all customers

### 2.1 - Running API instructions

#### 2.1.1 - Running API by Dockerfile

- After cloning the repository open command tools at the directory Customer\src\Customer

- Run the command ```docker build -t customers-api .``` to build the container image

- Once image created run the command docker run ``` --name customers-api-server -p 8080:8080 -d customers-api ``` to create the container\
If container is already created just run  ``` docker start customers-api-server ```

The api will be listening the port 8080 

#### 2.1.2 Running API by dotnet run
- Access the directory Customer\src\Customer\Customers.Api and run the command ``` dotnet run ```

-- API will be listening at the port 8080

### 2.2 - Customer API simulator

Customer API simulator is a console application project that send requests to creating new customers (POST) and GET customers that was created and list at console.

#### 2.2.1 Running API by dotnet run

- Starting API to run at port 8080
- Open command tool and access the directory Customer\src\Customer\CustomerSimulator
- run the command ``` dotnet run ```

## 3 Technologies implemented

- ASP.NET 8.0
- Swagger
- XUnit

## 4 Api Project Structure
 
 - CustomExceptions
 - Extensions
 - Domain (Domain entities, interface, services e validators)
 - Mappers
 - Middleware  
 - Persistence (repositories)
 
 
