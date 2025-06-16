# Event Ticketing System

This project implements a simple event listing page and a sales summary page, powered by an ASP.NET Core Web API with NHibernate for data access. It demonstrates key full-stack development skills, including API development, data persistence, frontend interaction, dependency injection, and unit testing.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Database Setup](#database-setup)
  - [Backend (ASP.NET Core API)](#backend-aspnet-core-api)
  - [Frontend (JavaScript)](#frontend-javascript)
- [API Endpoints](#api-endpoints)
- [Usage](#usage)
- [Unit Tests](#unit-tests)
- [Error Handling](#error-handling)
- [System Design & Troubleshooting](#system-design--troubleshooting)
- [Deliverables](#deliverables)

## Features

-   **Event Listing Page:** Displays upcoming events, allowing users to view event details.
    -   Events can be filtered by upcoming intervals: 30, 60, or 180 days.
    -   Table allows sorting by event start date or name.
-   **Sales Summary Page:** Renders a table of the top 5 events by sales.
-   **RESTful API:** ASP.NET Core Web API to serve event and sales data.
-   **Data Persistence:** Utilizes NHibernate for robust data access with SQLite.
-   **Dependency Injection:** Leverages .NET's built-in dependency injection for modular and testable code.

## Technologies Used

-   **Backend:**
    -   ASP.NET Core (.NET 8)
    -   NHibernate (for ORM)
    -   SQLite (Database)
    -   MSTest / NUnit (for Unit Testing)
-   **Frontend:**
    -   HTML5
    -   CSS3
    -   JavaScript (for dynamic content and API interaction)
-   **Other Tools:**
    -   Visual Studio / VS Code
    -   Git / GitHub

## Project Structure

akashshah1997-event_project/
├── Event_Project.sln
├── skillsAssessmentEvents.db           # SQLite database file
├── EventProject.Api/                   # ASP.NET Core Web API project
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── EventProject.Api.csproj
│   ├── NHibernateHelper.cs             # NHibernate session factory helper
│   ├── Program.cs                      # API startup and configuration
│   ├── Controllers/
│   │   └── EventsController.cs         # API controller for events
│   ├── Properties/
│   │   └── launchSettings.json
│   └── wwwroot/                        # Static files for the frontend
│       ├── app.js
│       ├── index.html                  # Main event listing page
│       ├── sales.html                  # Sales summary page
│       ├── sales.js
│       └── style.css
├── EventProject.Core/                  # Core entities, interfaces, and service implementations
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── EventProject.Core.csproj
│   ├── EventService.cs                 # Implementation of IEventService
│   ├── IEventService.cs                # Interface for Event Service
│   ├── ITicketService.cs               # Interface for Ticket Service
│   ├── Program.cs
│   ├── TicketService.cs                # Implementation of ITicketService
│   ├── Entities/
│   │   ├── Events.cs                   # Event entity definition
│   │   └── TicketSales.cs              # TicketSales entity definition
│   ├── Mappings/
│   │   ├── Event.hbm.xml               # NHibernate mapping for Events
│   │   └── TicketSales.hbm.xml         # NHibernate mapping for TicketSales
│   └── Properties/
│       └── launchSettings.json
└── EventProject.Tests/                 # Unit test project
├── EventProject.Tests.csproj
├── ServiceTests.cs                 # Example unit tests for services
└── Properties/
└── launchSettings.json


## Setup Instructions

Follow these steps to get the project up and running on your local machine.

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Node.js](https://nodejs.org/en/download/) (if running frontend separately or needing npm)
-   A code editor like [Visual Studio](https://visualstudio.microsoft.com/vs/) or [VS Code](https://code.visualstudio.com/)

### Database Setup

1.  **Download the SQLite Database:** The database file is a crucial part of this project. Download it from the provided Google Drive link:
    [https://drive.google.com/file/d/1j6lVv4vv5ltTkBjL2R8aAw1wWyGQXvQY/view?usp=sharing](https://drive.google.com/file/d/1j6lVv4vv5ltTkBjL2R8aAw1wWyGQXvQY/view?usp=sharing)
2.  **Place the Database File:** After downloading, place the `skillsAssessmentEvents.db` file directly into the **root directory** of the project (i.e., `akashshah1997-event_project/`).
3.  **Connection String:** Verify that the connection string in `EventProject.Api/appsettings.json` points to this database file correctly. It should look something like this:
    ```json
    "ConnectionStrings": {
        "SQLiteConnection": "Data Source=skillsAssessmentEvents.db;"
    }
    ```

### Backend (ASP.NET Core API)

1.  **Navigate to the API Project:**
    ```bash
    cd akashshah1997-event_project/EventProject.Api
    ```
2.  **Restore Dependencies:**
    ```bash
    dotnet restore
    ```
3.  **Build the Project:**
    ```bash
    dotnet build
    ```
4.  **Run the API:**
    ```bash
    dotnet run
    ```
    The API will typically run on `https://localhost:7000` (or a similar port). You can verify it by navigating to `/swagger` to see the API documentation.

### Frontend (JavaScript)

The frontend for this project is implemented using vanilla JavaScript, HTML, and CSS. It's designed to be served directly via the ASP.NET Core application.

1.  **Locate Frontend Files:** The frontend files are located in the `wwwroot` folder of the `EventProject.Api` project.
2.  **Access:** Once the backend API is running, you can typically access the frontend by navigating to the base URL of your API (e.g., `https://localhost:7000`). The `index.html` will be served by default.

## API Endpoints

The API exposes the following primary endpoint:

-   **`/api/events`**:
    -   **Method:** `GET`
    -   **Parameters:** Expects query parameters for filtering upcoming events (e.g., `days=30`, `days=60`, `days=180`).
    -   **Returns:** A JSON array of upcoming events, including Id, Name, StartsOn, EndsOn, and Location.
-   **`/api/ticketsales/top5`**:
    -   **Method:** `GET`
    -   **Returns:** A JSON array of the top 5 events by ticket sales.

## Usage

Once the API and frontend are running:

1.  **Event Listing:** Navigate to the main page (e.g., `https://localhost:7000/`) to view the event listing table. Use the provided filters to see events within specific upcoming day ranges.
2.  **Sorting:** Click on the table headers for "Event Start Date" or "Event Name" to sort the events accordingly.
3.  **Sales Summary:** Navigate to the sales summary page (e.g., `https://localhost:7000/sales.html`) to view the table of top 5 events by sales.

## Unit Tests

The project includes a dedicated unit test project (`EventProject.Tests`).

-   Tests are implemented using MSTest (or NUnit, as per preference).
-   They cover the backend services and repository logic to ensure correctness and maintainability.

To run the unit tests:

1.  **Navigate to the Test Project Directory:**
    ```bash
    cd akashshah1997-event_project/EventProject.Tests
    ```
2.  **Run Tests:**
    ```bash
    dotnet test
    ```

## Error Handling

Robust error handling is implemented within the API to gracefully manage failures, such as database connection issues, invalid requests, or unexpected server errors. API responses will include appropriate HTTP status codes and informative error messages where applicable.

## System Design & Troubleshooting

Not done

### Common Troubleshooting (Date/Time Mismatches)

During development, type conversion issues between C# `DateTime` and SQLite `DATETIME` columns (which often store as `TEXT`) were a common challenge. This was resolved by:
-   **Custom NHibernate User Type:** A `SQLiteDateTimeUserType` was implemented within `EventProject.Core/Mapping/Types/` to explicitly handle the conversion of `System.DateTime` to ISO 8601 strings for storage in SQLite's `DATETIME` (TEXT-compatible) columns, and vice-versa when reading.
-   **Direct C# Conversion:** Ensuring that whenever a `DateTime` property is assigned to a `string` property in C# code (e.g., for DTOs or UI display), `ToString()` is explicitly called (e.g., `myDateTime.ToString("o")` for ISO 8601 or `myDateTime.ToString("yyyy-MM-dd")` for a specific format).

## Deliverables

-   Complete source code hosted on GitHub.
-   This `README.md` file, detailing the project, setup, and usage.
-   Well-documented code following best practices.
-   Implemented unit tests for backend logic.

---
**Thank you for reviewing this project!**
