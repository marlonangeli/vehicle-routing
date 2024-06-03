FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/VehicleRouting.Api/VehicleRouting.Api.csproj", "src/VehicleRouting.Api/"]
COPY ["src/VehicleRouting.Application/VehicleRouting.Application.csproj", "src/VehicleRouting.Application/"]
COPY ["src/VehicleRouting.Domain/VehicleRouting.Domain.csproj", "src/VehicleRouting.Domain/"]
COPY ["src/VehicleRouting.Infrastructure/VehicleRouting.Infrastructure.csproj", "src/VehicleRouting.Infrastructure/"]
RUN dotnet restore "src/VehicleRouting.Api/VehicleRouting.Api.csproj"
COPY . .
WORKDIR "/src/src/VehicleRouting.Api"
RUN dotnet build "VehicleRouting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VehicleRouting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleRouting.Api.dll"]
