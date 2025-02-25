# Use .NET 9.0 SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

# Copy everything and publish the app
COPY . .
RUN dotnet publish -c Release -o out

# Use .NET 9.0 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/out .

# Expose port 5000 inside the container
EXPOSE 5000

# Set the entry point for the container and bind to all network interfaces
CMD ["dotnet", "MyCrudApi.dll", "--urls", "http://0.0.0.0:5000"]
