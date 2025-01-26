# MiniECommerce
A mini ECommerce system with Asp.NET Core build by Kahraman-Murat Enterprise Solutions

This project provides a robust solution for building web applications with a focus on scalability, maintainability, and seamless user experience.

## Key Features

- **Frontend**: 
  - Built with **Angular**, **Material UI**, and **Bootstrap**.
  - User interaction improvements using **FluentValidation**, **List Pagination**, **NgxSpinner**, **Alertify**, and **Toastr**.

- **Backend Architecture**: 
  - Implements **Onion Architecture**, **Generic Repository Pattern**, **CQRS**, and **Mediator Pattern**.
  - Dependency management with **Dependency Injection**.
  - CORS compliance for secure backend interactions.

- **Storage**:
  - Local file storage alongside **Azure Blob Storage** integration.

- **Database Management**:
  - Supports **RDMS**, **TPT**, **TPH** with **Entity Framework Code-First** and migrations.
  - Works with **PostgreSQL** or other DBs using **Docker**.

- **Authentication & Authorization**:
  - Includes **JWT Bearer Token**, **RefreshToken**, and **Guard Mechanism**.
  - Integration of **Google Login** and **Facebook Login**.
  - Built on **AspNetUser Identity** infrastructure.

- **Password Management**:
  - Password reset via **Mail Service** with notifications using **SignalR**.

- **Testing**:
  - Comprehensive testing with **Unit Tests**, **API Tests**, and tools like **Swagger** and **Postman**.

- **Error Handling & Logging**:
  - Managed with **HttpInterceptor**, **Global Http Error Handler**, and **Global Exception Handler**.
  - Detailed logging and monitoring using **HttpLogging**, **SeriLog**, and **Seq**.

## Tools and Technologies Used

- **Frontend**: Angular, Material UI, Bootstrap.
- **Backend**: ASP.NET Core, Entity Framework Core, CQRS, Mediator Pattern.
- **Database**: PostgreSQL, Docker.
- **Storage**: Azure Blob Storage.
- **Authentication**: JWT, AspNet Identity, Google & Facebook Login.
- **Testing**: Swagger, Postman.
- **Logging**: SeriLog, Seq.
