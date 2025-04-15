FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["APIAUTH.Server/APIAUTH.Server.csproj", "APIAUTH.Server/"]
COPY ["APIAUTH.Aplication/APIAUTH.Aplication.csproj", "APIAUTH.Aplication/"]
COPY ["APIAUTH.Domain/APIAUTH.Domain.csproj", "APIAUTH.Domain/"]
COPY ["APIAUTH.Data/APIAUTH.Data.csproj", "APIAUTH.Data/"]
COPY ["APIAUTH.Infrastructure/APIAUTH.Infrastructure.csproj", "APIAUTH.Infrastructure/"]

RUN dotnet restore "./APIAUTH.Server/APIAUTH.Server.csproj"
COPY . .

WORKDIR "/src/APIAUTH.Server"
RUN dotnet build "./APIAUTH.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIAUTH.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIAUTH.Server.dll"]
