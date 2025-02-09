# Commands
- `git log --all --graph --decorate --oneline` # Show the history of the repository
- `git checkout <commit>` # Move to a specific commit

# What is Microservices?
- Microservices is an architectural style that structures an application as a collection of services that are:
  - Highly maintainable and testable
  - Loosely coupled
  - Independently deployable
  - Organized around business capabilities
  - Owned by a small team
- Disadvantages in one sentence: Microservices are complex to manage and require a lot of effort to maintain.

### Docker and Microservices
- Docker is a tool that allows you to create, deploy, and run applications using containers.
- Containers are lightweight, standalone, and executable packages of software that include everything needed to run an application.


# ASP.NET Core
### Create a new project
- `dotnet new webapi -n <project-name>` # Create a new Web API project
- `dotnet new sln -n <solution-name>` # Create a new solution
- `dotnet sln add <project-name>` # Add a project to the solution
- `dotnet run` # Run the project
- `dotnet watch run` # Run the project and watch for changes
- `dotnet add package <package-name>` # Add a package to the project
- `dotnet sln add <project-name>` # Add a project to the solution
Genereate a new solution and add project to it
```bash
dotnet new sln
dotnet new webapi -n <project-name>
dotnet sln add <project-name>
```

## Breakdown
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework> # Target framework
    <Nullable>enable</Nullable> # Enable nullable reference types, which is a feature that helps you avoid null reference exceptions, a common source of bugs in C# programs.
    <ImplicitUsings>enable</ImplicitUsings> # Enable implicit usings, which is a feature that allows you to use types from the System namespace without adding a using directive.
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" /> # Add the Swashbuckle.AspNetCore package to the project, which is used for swagger documentation.
  </ItemGroup>

</Project>


# Entity Framework Core
- Entity Framework Core is an object-relational mapping (ORM) framework that allows you to interact with a database using .NET objects.
- An entity is a class that represents a table in the database.

## Nuget Packages
- `Microsoft.EntityFrameworkCore` # The core package for Entity Framework Core, 这是用来操作数据库的核心包，包含了所有的基本功能，比如增删改查等。
- postgresql # The PostgreSQL provider for Entity Framework Core, 这是用来操作PostgreSQL数据库的包。

# PostgreSQL
- PostgreSQL is an open-source relational database management system that is known for its reliability, robustness, and performance.
- To connect to a PostgreSQL database, you need to install the Npgsql.EntityFrameworkCore.PostgreSQL package.
- 