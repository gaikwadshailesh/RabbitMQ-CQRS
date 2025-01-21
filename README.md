# RabbitMQ CQRS Demo

A .NET Core demonstration project showcasing the implementation of CQRS pattern with RabbitMQ message broker integration.

## Project Structure

The solution follows Clean Architecture principles and consists of four projects:

- **RabbitMqCqrsDemo.Domain**: Contains domain entities and business logic
- **RabbitMqCqrsDemo.Application**: Houses CQRS commands, queries, and handlers
- **RabbitMqCqrsDemo.Infrastructure**: Implements RabbitMQ integration and repositories
- **RabbitMqCqrsDemo.Api**: Web API endpoints and dependency configuration

## Prerequisites

- .NET 7.0 SDK
- RabbitMQ Server (can be run using Docker)
- Visual Studio 2022 or VS Code

## Getting Started

1. Clone the repository