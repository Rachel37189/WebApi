# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["WebApiShop.sln", "."]
COPY ["WebApiShop/WebApiShop.csproj", "WebApiShop/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["DTOs/DTOs.csproj", "DTOs/"]
COPY ["Tests/Tests.csproj", "Tests/"]

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Build the solution
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebApiShop/WebApiShop.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Expose port
EXPOSE 80
EXPOSE 443

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Entry point
ENTRYPOINT ["dotnet", "WebApiShop.dll"]
