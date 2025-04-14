FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["APIAUTH.Server/APIAUTH.Server.csproj", "APIAUTH.Server/"]
COPY ["APIAUTH.Data/APIAUTH.Data.csproj", "APIAUTH.Data/"]
COPY ["APIAUTH.Application/APIAUTH.Application.csproj", "APIAUTH.Application/"]
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
