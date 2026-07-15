<div align="center">

# 🕌 Islamic Platform API

### A Production-Ready Islamic RESTful API Built with ASP.NET Core 8

A comprehensive backend platform that provides the **Holy Quran**, **Hadith Collections**, **Daily Azkar**, **Audio Recitations**, **Prayer Times**, **AI-Powered Islamic Assistant**, **Authentication**, **Bookmarks**, **Reading Progress**, and an **Admin Management System**.

Built with **ASP.NET Core 8**, **Clean Architecture**, **Entity Framework Core**, **SQL Server**, **Redis**, and **JWT Authentication**.

<p>

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?style=for-the-badge&logo=redis)
![JWT](https://img.shields.io/badge/JWT-Authentication-black?style=for-the-badge)
![Clean Architecture](https://img.shields.io/badge/Clean-Architecture-success?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)

</p>

</div>

---

> **A modern backend API that brings together the most essential Islamic services into one scalable platform following Clean Architecture and SOLID principles.**

---

# 📚 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Project Structure](#-project-structure)
- [Domain Model](#-domain-model)
- [Core Modules](#-core-modules)
- [Security](#-security)
- [Performance](#-performance)
- [External Integrations](#-external-integrations)
- [Database Seeding](#-database-seeding)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [API Overview](#-api-overview)
- [Testing](#-testing)
- [Screenshots](#-screenshots)
- [Demo](#-demo)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)

---

# 🌙 Overview

Islamic Platform API is a production-ready RESTful API designed to serve as the backend foundation for modern Islamic applications.

Instead of focusing on a single feature, the platform combines multiple Islamic services into one unified system, making it easy for web, mobile, or desktop applications to consume reliable Islamic content through secure and scalable APIs.

The project was built using **Clean Architecture**, ensuring complete separation between business logic, application services, infrastructure, and presentation layers. This architecture improves maintainability, scalability, and testability while keeping the codebase clean and easy to extend.

---

# ✨ Features

## 📖 Holy Quran

- Complete Quran (114 Surahs)
- Uthmani Script
- English Translation
- Tafseer Al-Muyassar
- Browse by Surah
- Browse by Juz
- Search Surahs
- Search Ayat
- Reading Progress
- Bookmark Verses
- Redis Caching

---

## 🎧 Audio Recitations

- 16+ Famous Quran Reciters
- Complete Audio Library
- High Quality Audio URLs
- Sheikh Profiles
- Cloudinary Image Management

---

## 🤲 Daily Azkar

Supported Categories

- Morning
- Evening
- Sleep
- Wake Up
- Prayer
- General

Features

- Smart Counter
- Progress Tracking
- Daily Reset
- Personalized Progress

---

## 📚 Hadith Collections

Includes the six authentic Hadith books:

- Sahih Al-Bukhari
- Sahih Muslim
- Sunan Abu Dawood
- Jami' At-Tirmidhi
- Sunan An-Nasa'i
- Sunan Ibn Majah

Features

- Browse Books
- Browse Chapters
- Browse Hadiths
- Search
- Pagination

---

## 🕌 Prayer Times

- Accurate Prayer Times
- Sunrise
- Next Prayer
- GPS Coordinates Support

---

## 🤖 AI Islamic Assistant

Powered by Google Gemini.

The assistant:

- Answers in Arabic
- Uses Quran & Sunnah only
- Provides Islamic References
- Avoids uncertain answers

---

## 🔐 Authentication

- Register
- Login
- JWT Authentication
- Refresh Token Rotation
- Multi-device Sessions
- Logout
- Logout All Devices

---

## ⭐ User Personalization

- Bookmark Ayat
- Bookmark Hadith
- Reading Progress
- Azkar Progress Tracking

---

## 👨‍💼 Administration

Administrators can manage:

- Sheikhs
- Hadith Books
- Hadith Chapters
- Hadiths
- Azkar

Role-based authorization is implemented using ASP.NET Identity.

---

# 🏗 Architecture

The project follows the principles of **Clean Architecture**.

```text
                 Clients
        (Web / Mobile / Desktop)
                    │
                    ▼
              ASP.NET Core API
                    │
                    ▼
          Application Layer
                    │
                    ▼
             Domain Layer
                    │
                    ▼
        Infrastructure Layer
                    │
     ┌──────────────┴──────────────┐
     ▼                             ▼
 SQL Server                    External APIs
                                   │
                     Redis • Cloudinary • Gemini
```

## Design Principles

- Clean Architecture
- SOLID Principles
- Dependency Injection
- Repository Pattern
- Unit of Work Pattern
- RESTful API Design
- Separation of Concerns
- Domain-Driven Design Concepts

---

# 🛠 Technology Stack

## Backend

| Technology | Version | Purpose |
|------------|---------|---------|
| ASP.NET Core | .NET 8 | REST API |
| Entity Framework Core | 8 | ORM |
| SQL Server | 2019+ | Primary Database |
| ASP.NET Identity | 8 | Authentication |
| JWT Bearer | 8 | Authorization |
| Redis | 7 | Distributed Caching |
| Cloudinary | Latest | Image Storage |
| xUnit | Latest | Unit Testing |
| Moq | Latest | Mocking |

---

## Architecture & Patterns

- Clean Architecture
- Repository Pattern
- Unit of Work
- Dependency Injection
- SOLID Principles
- Manual Mapping
- Global Exception Middleware

---

# 📂 Project Structure

```text
IslamicPlatform

├── IslamicPlatform.Api
│   ├── Controllers
│   ├── Middleware
│   ├── Extensions
│   └── Program.cs
│
├── IslamicPlatform.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Common
│
├── IslamicPlatform.Domain
│   ├── Entities
│   ├── Enums
│   └── Contracts
│
├── IslamicPlatform.Infrastructure
│   ├── Persistence
│   ├── Repositories
│   ├── Services
│   ├── Seeders
│   └── Migrations
│
└── IslamicPlatform.Tests
```

## Project Responsibilities

| Project | Responsibility |
|----------|---------------|
| Domain | Business Rules & Entities |
| Application | Services, DTOs & Interfaces |
| Infrastructure | Database, Repositories & External Services |
| API | REST Controllers & Middleware |
| Tests | Unit Testing |

# 🏛 Domain Model

The Domain layer represents the heart of the application. It contains all business entities, domain rules, and core models without any dependency on infrastructure or external frameworks.

The project is designed around rich domain models that clearly separate business logic from technical implementation.

---

## Domain Entities

### 📖 Quran

| Entity | Description |
|---------|-------------|
| Surah | Represents a Quran chapter |
| Ayah | Represents a Quran verse |
| AyahTranslation | English translation of a verse |
| Tafseer | Tafseer Al-Muyassar for each verse |

---

### 🎧 Audio

| Entity | Description |
|---------|-------------|
| Sheikh | Quran reciter information |
| Recitation | Audio file for a Surah |

---

### 🤲 Azkar

| Entity | Description |
|---------|-------------|
| Zikr | Daily remembrance |
| UserZikrProgress | User progress for each Zikr |

---

### 📚 Hadith

| Entity | Description |
|---------|-------------|
| HadithBook | Hadith collection |
| HadithChapter | Chapter inside a book |
| Hadith | Individual Hadith |

---

### 👤 Identity

| Entity | Description |
|---------|-------------|
| ApplicationUser | ASP.NET Identity user |
| RefreshToken | Refresh token management |
| Bookmark | User bookmarks |
| ReadingProgress | Continue reading progress |

---

# ⚙ Core Modules

The API is organized into independent modules, each responsible for a specific business domain.

---

## 📖 Quran Module

Provides complete access to the Holy Quran.

### Features

- Browse all Surahs
- Browse by Juz
- Retrieve Surah Ayat
- Translation
- Tafseer
- Verse Search
- Surah Search
- Continue Reading
- Bookmarks
- Redis Cache

---

## 🎧 Audio Module

Provides Quran recitations from famous reciters.

### Features

- 16+ Famous Sheikhs
- Audio Streaming URLs
- Sheikh Profiles
- Surah Audio
- Cloudinary Integration

---

## 🤲 Azkar Module

Daily remembrance system with personalized progress tracking.

Supported Categories

- Morning
- Evening
- Sleep
- Wake Up
- Prayer
- General

Features

- Smart Counter
- Progress Tracking
- Daily Reset
- User History

---

## 📚 Hadith Module

Provides authenticated Hadith collections.

Features

- Browse Books
- Browse Chapters
- Browse Hadiths
- Search
- Pagination
- Book Relationships

---

## 🕌 Prayer Times Module

Provides prayer times based on geographic coordinates.

Features

- Daily Prayer Times
- Sunrise
- Next Prayer
- GPS Coordinates
- External API Integration

---

## 🤖 AI Assistant Module

An AI-powered Islamic assistant built on Google Gemini.

Capabilities

- Arabic Responses
- Quran-Based Answers
- Sunnah-Based Answers
- Islamic References
- Safe Prompt Engineering

---

## 🔐 Authentication Module

Authentication is implemented using ASP.NET Identity and JWT.

Features

- Register
- Login
- Refresh Tokens
- Logout
- Logout All Devices
- Multiple Sessions

---

## ⭐ User Module

Personalized features for authenticated users.

- Bookmark Ayat
- Bookmark Hadith
- Reading Progress
- Azkar Progress

---

## 👨‍💼 Administration Module

Role-based administration system.

Administrators can manage:

- Sheikhs
- Hadith Books
- Hadith Chapters
- Hadiths
- Azkar

---

# 🔐 Security

Security was considered from the beginning of the project.

---

## Authentication

- ASP.NET Identity
- JWT Bearer Authentication
- Refresh Token Rotation
- Secure Password Hashing

---

## Authorization

- Role-Based Authorization
- User Role
- Admin Role

---

## Refresh Token Rotation

The API implements Refresh Token Rotation to improve security.

Each refresh request:

- Generates a new Access Token
- Generates a new Refresh Token
- Revokes the previous Refresh Token

This approach helps prevent replay attacks and improves session security.

---

## API Protection

Additional protection mechanisms include:

- Rate Limiting
- Global Exception Middleware
- Input Validation
- Secure Random Token Generation

---

# ⚡ Performance

Performance optimization was an important goal throughout development.

---

## Redis Distributed Cache

Frequently requested data is cached to reduce database load.

| Cached Data | Strategy |
|--------------|----------|
| Quran Surahs | Redis |
| Surah Ayat | Redis |
| Azkar Categories | Redis |

---

## Pagination

Pagination is implemented for Hadith collections to improve scalability and reduce response payload size.

---

## Entity Framework Optimization

The project uses several optimization techniques:

- Async Database Operations
- Optimized LINQ Queries
- Proper Navigation Properties
- Efficient Includes
- Composite Indexes
- Unique Constraints

---

## Error Handling

A custom Global Exception Middleware provides:

- Consistent Error Responses
- Centralized Logging
- Cleaner Controllers
- Better API Experience

---

# 🌐 External Integrations

The application integrates with several trusted external services.

| Service | Purpose |
|----------|----------|
| AlQuran Cloud | Quran Text |
| Quran.com API | Audio Recitations |
| Aladhan API | Prayer Times |
| Google Gemini | AI Assistant |
| Cloudinary | Image Storage |
| Hadith JSON Dataset | Hadith Collections |

---

# 🌱 Database Seeding

The application automatically seeds the database on the first startup.

Included seeders:

- Quran Seeder
- Sheikh Seeder
- Hadith Seeder
- Azkar Seeder
- Role Seeder

The seeded data includes:

- Complete Quran
- English Translation
- Tafseer Al-Muyassar
- 16+ Famous Sheikhs
- Six Hadith Books
- 50,000+ Hadiths
- 100 Daily Azkar
- Default Roles
- Default Administrator

---

# 🗄 Database

The application uses **Entity Framework Core Code First** with SQL Server.

Key Features

- Code First Migrations
- Fluent API Configuration
- Composite Keys
- Relationships
- Indexes
- Constraints
- Seed Data

# 🚀 Getting Started

## Prerequisites

Before running the project, make sure you have the following installed:

- .NET 8 SDK
- SQL Server (or LocalDB)
- Redis Server
- Visual Studio 2022 (Recommended)
- Git

---

## Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/IslamicPlatform.git

cd IslamicPlatform
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Configure User Secrets

Store sensitive values using **User Secrets** during development or **Environment Variables** in production.

Example:

```text
ConnectionStrings__DefaultConnection
Jwt__Key
Jwt__Issuer
Jwt__Audience

Cloudinary__CloudName
Cloudinary__ApiKey
Cloudinary__ApiSecret

Gemini__ApiKey
```

> **Important:** Never commit secrets or API keys to GitHub.

---

## Apply Database Migrations

```bash
dotnet ef database update
```

---

## Run Redis

Make sure Redis is running before starting the application.

---

## Start the API

```bash
dotnet run
```

When the application starts for the first time, the database will be seeded automatically.

> **Note:** Initial seeding may take several minutes because Quran, Hadith, and Recitation data are downloaded from trusted external sources.

---

# ⚙ Configuration

The project uses the following configuration files:

- `appsettings.json`
- `appsettings.Development.json`
- User Secrets
- Environment Variables

Sensitive values should always be stored outside the repository.

---

# 📡 API Overview

The API is organized into feature-based modules.

| Module | Description |
|---------|-------------|
| Authentication | User registration, login, JWT & Refresh Tokens |
| Quran | Surahs, Ayat, Tafseer & Search |
| Audio | Quran Recitations |
| Azkar | Daily Remembrance |
| Hadith | Books, Chapters & Hadith |
| Prayer Times | Prayer Times & Next Prayer |
| AI Assistant | Islamic AI Assistant |
| Bookmarks | User Bookmarks |
| Reading Progress | Continue Reading |
| Administration | Protected Management APIs |

The API follows RESTful principles with consistent JSON responses and proper HTTP status codes.

---

# 🧪 Testing

Unit tests are implemented using **xUnit** and **Moq**.

Covered services include:

- Authentication Service
- Quran Service
- Hadith Service
- Azkar Service

Run all tests:

```bash
dotnet test
```

---

# 📈 Performance Highlights

Several optimizations were implemented to improve performance and scalability.

✔ Redis Distributed Cache

✔ Asynchronous Programming

✔ Pagination

✔ Optimized Entity Framework Queries

✔ Repository Pattern

✔ Unit of Work

✔ Global Exception Middleware

✔ Rate Limiting

✔ JWT Authentication

✔ Refresh Token Rotation

---

# 📷 Screenshots

You can include screenshots such as:

- Swagger UI
- Authentication Endpoints
- Quran Endpoints
- Hadith Endpoints
- SQL Server Database Diagram
- Unit Test Results

---

# 🎥 Demo

A complete walkthrough of the project is available here.

> **Demo Video:** *(Add your Google Drive or YouTube link here.)*

---

# 🚀 Future Improvements

Planned enhancements include:

- Docker Support
- API Versioning
- Health Checks
- CI/CD Pipeline
- OpenTelemetry
- Elasticsearch Integration
- Background Jobs (Hangfire)
- Email Notifications
- Multi-language Support
- Mobile Application Support

---

# 🤝 Contributing

Contributions are always welcome.

If you would like to contribute:

```bash
Fork the repository

Create a feature branch

git checkout -b feature/YourFeature

Commit your changes

git commit -m "Add new feature"

Push your branch

git push origin feature/YourFeature

Open a Pull Request
```

---

# 📄 License

This project is licensed under the **MIT License**.

Feel free to use, modify, and distribute this project according to the license terms.

---

# 🙏 Acknowledgements

Special thanks to the following services and projects that made this application possible:

- AlQuran Cloud
- Quran.com
- Aladhan API
- Google Gemini
- Cloudinary
- Microsoft ASP.NET Core
- Entity Framework Core
- Redis
- AhmedBaset Hadith Dataset

---

# 👨‍💻 About the Author

## Mohamed Samir

**Junior .NET Backend Developer**

Passionate about building scalable backend systems using modern Microsoft technologies and software architecture principles.

### Technical Interests

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Clean Architecture
- RESTful APIs
- Authentication & Authorization
- System Design
- Software Architecture

---

### Connect with Me

- 💼 LinkedIn: https://www.linkedin.com/in/mohamed-samir-4014a1311/
- 💻 GitHub: https://github.com/

---

<div align="center">

## ⭐ If you found this project useful, consider giving it a Star!

Your support helps the project reach more developers and motivates future improvements.

---

**Designed & Developed with ❤️ by Mohamed Samir**

ASP.NET Core • Clean Architecture • RESTful APIs

> **﴿ وَقُل رَّبِّ زِدْنِي عِلْمًا ﴾**
>
> **سورة طه - الآية 114**

</div>
