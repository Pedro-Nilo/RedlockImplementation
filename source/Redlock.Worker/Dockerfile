# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source/Redlock.Worker

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app

# Final stage
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Redlock.Worker.dll"]
