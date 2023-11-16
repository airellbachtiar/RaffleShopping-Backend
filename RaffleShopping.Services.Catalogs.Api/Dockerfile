#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RaffleShopping.Services.Catalogs.Api/RaffleShopping.Services.Catalogs.Api.csproj", "RaffleShopping.Services.Catalogs.Api/"]
COPY ["RaffleShopping.Services.Catalogs/RaffleShopping.Services.Catalogs.csproj", "RaffleShopping.Services.Catalogs/"]
RUN dotnet restore "RaffleShopping.Services.Catalogs.Api/RaffleShopping.Services.Catalogs.Api.csproj"
COPY . .
WORKDIR "/src/RaffleShopping.Services.Catalogs.Api"
RUN dotnet build "RaffleShopping.Services.Catalogs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaffleShopping.Services.Catalogs.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaffleShopping.Services.Catalogs.Api.dll"]