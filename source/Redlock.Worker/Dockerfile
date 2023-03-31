# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . .
RUN dotnet restore "Redlock.Worker/Redlock.Worker.csproj"

COPY . .
RUN dotnet publish "Redlock.Worker/Redlock.Worker.csproj" -c release -o /app

# Final stage
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Redlock.Worker.dll"]
