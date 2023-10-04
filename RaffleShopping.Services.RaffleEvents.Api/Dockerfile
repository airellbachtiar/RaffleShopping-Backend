#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RaffleShopping.Services.RaffleEvents.Api/RaffleShopping.Services.RaffleEvents.Api.csproj", "RaffleShopping.Services.RaffleEvents.Api/"]
COPY ["RaffleShoppping.Services.RaffleEvents/RaffleShoppping.Services.RaffleEvents.csproj", "RaffleShoppping.Services.RaffleEvents/"]
RUN dotnet restore "RaffleShopping.Services.RaffleEvents.Api/RaffleShopping.Services.RaffleEvents.Api.csproj"
COPY . .
WORKDIR "/src/RaffleShopping.Services.RaffleEvents.Api"
RUN dotnet build "RaffleShopping.Services.RaffleEvents.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaffleShopping.Services.RaffleEvents.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaffleShopping.Services.RaffleEvents.Api.dll"]