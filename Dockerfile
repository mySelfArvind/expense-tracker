# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY ["ExpenseTracker.csproj", "./"]

# Restore dependencies
RUN dotnet restore "ExpenseTracker.csproj"

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish "ExpenseTracker.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published application
COPY --from=build /app/publish .

# Render provides the PORT environment variable
ENV ASPNETCORE_URLS=http://+:${PORT}

# Start application
ENTRYPOINT ["dotnet", "ExpenseTracker.dll"]