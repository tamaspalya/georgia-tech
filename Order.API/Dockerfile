#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Order.API/Order.API.csproj", "Order.API/"]
COPY ["Order.Application/Order.Application.csproj", "Order.Application/"]
COPY ["Order.Infrastructure/Order.Infrastructure.csproj", "Order.Infrastructure/"]
COPY ["Order.Domain/Order.Domain.csproj", "Order.Domain/"]
COPY ["EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
RUN dotnet restore "./Order.API/./Order.API.csproj"
COPY . .
WORKDIR "/src/Order.API"
RUN dotnet build "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]