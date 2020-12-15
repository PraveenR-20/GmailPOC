FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["./BeyondTrust.Dragon.Tenant.API/BeyondTrust.Dragon.Tenant.API.csproj", "./BeyondTrust.Dragon.Tenant.API/"]
COPY ["./BeyondTrust.Dragon.Tenant.Application/BeyondTrust.Dragon.Tenant.Application.csproj", "./BeyondTrust.Dragon.Tenant.Application/"]
COPY ["./BeyondTrust.Dragon.Tenant.Data/BeyondTrust.Dragon.Tenant.Data.csproj", "./BeyondTrust.Dragon.Tenant.Data/"]
COPY ["./BeyondTrust.Dragon.Tenant.IOC/BeyondTrust.Dragon.Tenant.IoC.csproj", "./BeyondTrust.Dragon.Tenant.IOC/"]
RUN dotnet restore "./BeyondTrust.Dragon.Tenant.API/BeyondTrust.Dragon.Tenant.API.csproj"
COPY . .
WORKDIR "./BeyondTrust.Dragon.Tenant.API"
RUN dotnet build "./BeyondTrust.Dragon.Tenant.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./BeyondTrust.Dragon.Tenant.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app  
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeyondTrust.Dragon.Tenant.API.dll"]
