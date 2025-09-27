
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY WebMotors.sln .
COPY WebMotors/*.csproj ./WebMotors/
COPY Domain/*.csproj ./Domain/
COPY Infra/*.csproj ./Infra/

RUN dotnet restore WebMotors.sln

COPY . .

WORKDIR /src/WebMotors

RUN dotnet publish "WebMotors.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebMotors.dll"]
