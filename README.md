## Anruki Shop API

Anruki Shop API is a backend system for an e-commerce platform built using ASP.NET Core Web API. The project follows Clean Architecture principles and SOLID design patterns to ensure scalability, maintainability, and clear separation of concerns.

## Overview

The API provides core functionalities required for an online store, including product management, order processing, payments, authentication, and search capabilities. It is designed to be modular and easy to extend.

## Features

- User authentication and authorization using JWT  
- Product management (create, update, delete, activate)  
- Order management system  
- Payment processing logic  
- Product search using SQL Server stored procedures  
- Image handling and uploads  
- Email integration for notifications and verification  
- Structured error handling using custom exceptions  

## Architecture

The project is structured based on Clean Architecture:

### Domain Layer
Contains core business entities and domain logic with validation methods.

### Application Layer
Contains services, models, interfaces, and business use cases.

### Infrastructure Layer
Handles database access, external services, and integrations.

### API Layer
Exposes RESTful endpoints and handles HTTP requests and responses.

## Technologies

- C#  
- ASP.NET Core 8 Web API  
- SQL Server  
- Stored Procedures  
- JWT Authentication  
- RESTful API Design  

## Project Structure
AnrukiShopAPI/
├── Domain/
├── Application/
├── Infrastructure/
└── API/


## Getting Started

### 1. Clone the repository
https://github.com/AhmedSukary/anrukishopapi.git


### 2. Configure environment variables or appsettings.json

- Database connection string  
- JWT key  
- Email credentials  
- External service keys  

### 3. Run the project
dotnet run


## API Documentation

Swagger is available to test and explore the API endpoints:
https://anrukishopapi.runasp.net/swagger


## Notes

The project is designed to be scalable and maintainable.  
It follows a clear separation of concerns and uses custom exception handling to manage errors effectively.

## Contributing

Contributions are welcome. You can submit a pull request for improvements or new features.

## Contact

For any questions or inquiries, please contact:

ahmedsukaryy@gmail.com
