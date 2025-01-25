# Step 1: Use the .NET Core SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

# Copy the solution and project files
COPY ./VehicleApp.sln ./
COPY ./VehicleApp.Domain/VehicleApp.Domain.csproj ./VehicleApp.Domain/
COPY ./VehicleApp.Helper/VehicleApp.Helper.csproj ./VehicleApp.Helper/
COPY ./VehicleApp.Infrastructure/VehicleApp.Infrastructure.csproj ./VehicleApp.Infrastructure/
COPY ./VehicleApp/VehicleApp.Web.csproj ./VehicleApp/

# Restore all the dependencies
RUN dotnet restore

# Copy everything and build the application
COPY . ./
WORKDIR /app/VehicleApp
RUN dotnet publish -c Release -o out

# Step 2: Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/VehicleApp/out ./
ENTRYPOINT ["dotnet", "VehicleApp.Web.dll"]
