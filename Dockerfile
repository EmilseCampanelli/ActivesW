# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiamos primero los csproj
COPY APIAUTH.Server/APIAUTH.Server.csproj APIAUTH.Server/
COPY APIAUTH.Aplication/APIAUTH.Aplication.csproj APIAUTH.Aplication/
COPY APIAUTH.Data/APIAUTH.Data.csproj APIAUTH.Data/
COPY APIAUTH.Domain/APIAUTH.Domain.csproj APIAUTH.Domain/
COPY APIAUTH.Infrastructure/APIAUTH.Infrastructure.csproj APIAUTH.Infrastructure/

# Restauramos dependencias
RUN dotnet restore "APIAUTH.Server/APIAUTH.Server.csproj"

# Copiamos el resto
COPY . .

# Compilamos
WORKDIR /src/APIAUTH.Server
RUN dotnet build "APIAUTH.Server.csproj" -c Release -o /app/build

# Publicamos
RUN dotnet publish "APIAUTH.Server.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "APIAUTH.Server.dll"]
