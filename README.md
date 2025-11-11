# Employee Contact Management System

A full-stack application built with:

- **Database:** MS SQL Server (Database)**
- **Backend:** ASP.NET Core 8 Web API (Backend)**
- **Frontend:** React + TypeScript + Vite (Frontend)**
- **State/Forms:** Context API + React-Hook-Form**
- **UI Framework:** Bootstrap UI**
- **Additional Features:** Pagination + Search + Validation, Gravatar Integration, Toast Notifications, API Availability Banner

## Project Structure

```
/project-root
│
├── db/
│   ├── schema.sql
│   └── seed.sql
│
├── backend/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Services/
│   ├── Models/
│   ├── Data/
│   ├── Program.cs
│   ├── appsettings.json
│   └── ...
│
└── frontend/
    ├── src/
    │   ├── api/
    │   ├── components/
    │   ├── contexts/
    │   ├── hooks/
    │   ├── pages/
    │   ├── types/
    │   ├── App.tsx
    │   └── main.tsx
    │   └── index.css
    └── ...

```

---

## Features

- **CRUD Operations:** Add / Edit / Delete Employees.
- **Company Management:** Display Companies + Employee Company Assignment.
- **Search:** Text Search with **Debounced** input.
- **Performance:** Server-side Pagination.
- **UI/UX:** Toast Notifications, Form Validation, and Gravatar integration (for employee avatars).
- **Monitoring:** API Health Check Banner.

---

## Database Setup

1) Open **SQL Server Management Studio (SSMS)**
2) Run `schema.sql` to create tables, constrains and Indexes
3) Run `seed.sql` to insert sample data  

> **Note:**Ensure the backend connection string matches your SQL Server instance.

---

## Backend — ASP.NET Core 8 Web API

### Setup (Visual Studio 2022 Community)

1) Open **Visual Studio 2022 Community**
2) Click **Open a project or solution**.
6) Build the project to install all the required packages
7) Press **F5** to start the API

### Base URL
```
https://localhost:7204
```

---

## API Endpoints

### Employees:
- GET /api/employees
- GET /api/employees/{id}
- POST /api/employees
- PUT /api/employees/{id}
- DELETE /api/employees/{id}

### Companies:
- GET /api/companies

### Health:
```
- GET /health → Healthy
```

## Frontend — React + TypeScript + Vite

### Setup

```
cd frontend
npm install
npm run dev
```

Default URL:
```
http://localhost:5173
```

---

## Validation & Utilities

### Gravatar
Uses crypto-js MD5 to generate unique avatar URLs based on employee email addresses.


### Validation

#### Client-Side
- react-hook-form
- Required fields

#### Server-Side
- DTO validation
- Error messages returned to UI