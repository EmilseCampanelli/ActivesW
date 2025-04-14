FROM mcr.microsoft.com/dotnet/aspnet:7.0.15 AS base
FROM mcr.microsoft.com/dotnet/sdk:7.0.306 AS build

# Copiamos solo los archivos de proyectos para restaurar dependencias más rápido
COPY ["APIAUTH.Server/APIAUTH.Server.csproj", "APIAUTH.Server/"]
COPY ["APIAUTH.Aplication/APIAUTH.Aplication.csproj", "APIAUTH.Aplication/"]
COPY ["APIAUTH.Data/APIAUTH.Data.csproj", "APIAUTH.Data/"]
COPY ["APIAUTH.Domain/APIAUTH.Domain.csproj", "APIAUTH.Domain/"]
COPY ["APIAUTH.Infrastructure/APIAUTH.Infrastructure.csproj", "APIAUTH.Infrastructure/"]

# Restauramos dependencias
RUN dotnet restore "APIAUTH.Server/APIAUTH.Server.csproj"

COPY ["APIAUTH.Server/APIAUTH.Server.csproj", "APIAUTH.Server/"]
COPY ["APIAUTH.Aplication/APIAUTH.Aplication.csproj", "APIAUTH.Aplication/"]
COPY ["APIAUTH.Data/APIAUTH.Data.csproj", "APIAUTH.Data/"]
COPY ["APIAUTH.Domain/APIAUTH.Domain.csproj", "APIAUTH.Domain/"]
COPY ["APIAUTH.Infrastructure/APIAUTH.Infrastructure.csproj", "APIAUTH.Infrastructure/"]



# Build
WORKDIR /src/APIAUTH.Server
RUN dotnet build "APIAUTH.Server.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "APIAUTH.Server.csproj" -c Release -o /app/publish

# Etapa final: runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Puerto (por si Railway lo necesita explícito)
EXPOSE 80

# Aquí ejecutamos las migraciones
ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "APIAUTH.Server"]
