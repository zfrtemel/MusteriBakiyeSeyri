# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY ["MusteriBakiyeSeyri.sln", "./"]

# Copy project files
COPY ["Web/Web.csproj", "Web/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Tests/Application.UnitTests/Application.UnitTests.csproj", "Tests/Application.UnitTests/"]

# Restore packages
RUN dotnet restore "Web/Web.csproj"

# Copy source code
COPY . .

# Build and test
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

# Run tests
WORKDIR "/src"
RUN dotnet test "Tests/Application.UnitTests/Application.UnitTests.csproj" --no-restore --verbosity normal

# Publish
WORKDIR "/src/Web"
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install PostgreSQL client tools
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=build /app/publish .

# Create entrypoint script
COPY docker-entrypoint.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/docker-entrypoint.sh

EXPOSE 8080

ENTRYPOINT ["docker-entrypoint.sh"]
CMD ["dotnet", "Web.dll"]
