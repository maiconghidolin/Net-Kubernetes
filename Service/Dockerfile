# Use the official .NET 8 SDK image for building/publishing the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj and restore	
COPY ["Service.csproj", "."]
RUN dotnet restore "./Service.csproj"

# Copy the remaining files and publish
COPY . .
RUN dotnet publish "./Service.csproj" -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

# Use a lightweight runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /src

# Copy the published output 
COPY --from=build /src/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "Service.dll"]
