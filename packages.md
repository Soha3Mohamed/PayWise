# EF Core & SQL Server
dotnet add PayWise.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add PayWise.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add PayWise.Infrastructure package Microsoft.EntityFrameworkCore.Design

# FluentValidation
dotnet add PayWise.Api package FluentValidation.AspNetCore

# Serilog logging
dotnet add PayWise.Api package Serilog.AspNetCore
dotnet add PayWise.Api package Serilog.Sinks.Console
dotnet add PayWise.Api package Serilog.Sinks.File

# Swagger
dotnet add PayWise.Api package Swashbuckle.AspNetCore

# JWT Authentication
dotnet add PayWise.Api package Microsoft.AspNetCore.Authentication.JwtBearer

# Redis
dotnet add PayWise.Infrastructure package Microsoft.Extensions.Caching.StackExchangeRedis

# Unit Testing
dotnet add PayWise.Tests package xunit
dotnet add PayWise.Tests package Moq
