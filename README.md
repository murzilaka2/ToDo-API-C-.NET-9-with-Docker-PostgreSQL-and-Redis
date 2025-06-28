# ToDo API on .NET 9 with Docker, PostgreSQL, and Redis

**ToDo API** is a sample project demonstrating how to build a modern, containerized RESTful service using **ASP.NET Core 9.0**, **Entity Framework Core 9.0**, **PostgreSQL 15+**, and **Redis 7+** with **Docker** and **Docker Compose**. The project features automatic migrations, caching, inter-service communication, and interactive documentation via Swagger.

## Features

- **CRUD operations** for ToDo tasks:
  - `GET /api/tasks` — retrieve all tasks
  - `GET /api/tasks/{id}` — retrieve a task by ID
  - `POST /api/tasks` — create a new task
  - `PUT /api/tasks/{id}` — update an existing task
  - `DELETE /api/tasks/{id}` — delete a task
- **Caching** with Redis:
  - Task list cached for 5 minutes
  - Individual tasks cached for 2 minutes
- **EF Core Migrations**: automatic schema creation and application on startup
- **Swagger UI** available in development mode for interactive API exploration

## Prerequisites

- Docker & Docker Compose
- .NET SDK 9.0

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/murzilaka2/todo-api-docker.git
   cd TodoApi
2. Build and start the containers:
   ```bash
   docker-compose up --build
4. Send request:
   ```bash
   http://localhost:5000/api/tasks
   
