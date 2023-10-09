#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RaffleShopping.Services.Customers.Api/RaffleShopping.Services.Customers.Api.csproj", "RaffleShopping.Services.Customers.Api/"]
COPY ["RaffleShopping.Services.Customers/RaffleShopping.Services.Customers.csproj", "RaffleShopping.Services.Customers/"]
RUN dotnet restore "RaffleShopping.Services.Customers.Api/RaffleShopping.Services.Customers.Api.csproj"
COPY . .
WORKDIR "/src/RaffleShopping.Services.Customers.Api"
RUN dotnet build "RaffleShopping.Services.Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaffleShopping.Services.Customers.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaffleShopping.Services.Customers.Api.dll"]