# 🚗 CarSales API

CarSales is a modern Web API project for managing new and used car listings.  
Built with **.NET 9**, **Clean Architecture**, **CQRS**, and **MediatR**, it provides a scalable, testable, and maintainable backend foundation.

---

## 🧱 Architecture Overview

This project follows **Clean Architecture** principles and is divided into 4 key layers:

| Layer              | Responsibility                                                                 |
|--------------------|---------------------------------------------------------------------------------|
| `CarSales.API`      | Entry point. Hosts Controllers, Middleware, and base configurations.           |
| `CarSales.Application` | Business logic: Handlers (CQRS), Validators, Services Interfaces, Mappings. |
| `CarSales.Domain`   | Core entities, Enums, Interfaces, and Domain rules.                            |
| `CarSales.Infrastructure` | EF Core DbContext, Repositories, external services (e.g., Identity).      |

---

## ⚙️ Tech Stack

- **.NET 9**
- **CQRS + MediatR**
- **FluentValidation**
- **AutoMapper**
- **ASP.NET Core Identity (wrapped in IIdentityServices)**
- **Global Exception Middleware**
- **Custom `Result<T>` and `ApiResponse<T>` pattern**

---

## 🧠 Core Concepts

| Concept              | Purpose                                                                 |
|----------------------|-------------------------------------------------------------------------|
| `CQRS`               | Clean separation of write (Command) and read (Query) operations.        |
| `FluentValidation`   | Validates requests at the Application layer.                            |
| `Result<T>`          | Replaces exceptions with a standardized pattern for success/failure.    |
| `ApiResponse<T>`     | Wraps all HTTP responses from the API in a consistent format.           |
| `ErrorType Enum`     | Maps business errors to corresponding HTTP StatusCodes.                 |
| `IIdentityServices`  | Abstracts Identity logic from `UserManager` and `SignInManager`.        |
| `GlobalExceptionMiddleware` | Catches unhandled exceptions and formats them into standard error responses. |

---

## ✅ Implemented Features

| Feature                         | Status |
|----------------------------------|--------|
| Register new user                | ✅     |
| Login user and generate token    | ✅     |
| Validate email & password        | ✅     |
| Return structured error messages | ✅     |
| Abstract Identity dependencies   | ✅     |

---

## 🔐 Authentication Flow

1. `POST /api/auth/register`  
   - Validates user input with FluentValidation.  
   - Creates a user via `IIdentityServices`.  
   - Returns `Result<RegisterUserDto>` wrapped in `ApiResponse`.

2. `POST /api/auth/login`  
   - Verifies email exists via `GetUserByEmailAsync`.  
   - Validates password via `IsPasswordExist(...)`.  
   - Returns JWT & Refresh Token in `ResponseAuthModel`.

---

## 🧾 Result<T> & Error Handling

### `Result<T>` Pattern

```csharp
public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType? ErrorType { get; set; }

    public static Result<T> Success(T data) => new(true, data, null, null);
    public static Result<T> Failure(string error, ErrorType type) => new(false, default, error, type);
}
