FROM mcr.microsoft.com/dotnet/aspnet:7.0.15 AS base
FROM mcr.microsoft.com/dotnet/sdk:7.0.306 AS build

COPY ["APIAUTH.Server/APIAUTH.Server.csproj", "APIAUTH.Server/"]
COPY ["APIAUTH.Data/APIAUTH.Data.csproj", "APIAUTH.Data/"]
COPY ["APIAUTH.Aplication/APIAUTH.Aplication.csproj", "APIAUTH.Aplication/"]
COPY . .

WORKDIR "/src/APIAUTH.Server"
RUN dotnet restore

# Hacemos disponible dotnet ef
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Aquí ejecutamos las migraciones
ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "APIAUTH.Server"]
