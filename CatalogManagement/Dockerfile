FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/CatalogManagement.Api/CatalogManagement.Api.csproj", "CatalogManagement.Api/"]
COPY ["/src/CatalogManagement.Application/CatalogManagement.Application.csproj", "CatalogManagement.Application/"]
COPY ["/src/CatalogManagement.Domain/CatalogManagement.Domain.csproj", "CatalogManagement.Domain/"]
COPY ["/src/CatalogManagement.Contracts/CatalogManagement.Contracts.csproj", "CatalogManagement.Contracts/"]
COPY ["/src/CatalogManagement.Infrastructure/CatalogManagement.Infrastructure.csproj", "CatalogManagement.Infrastructure/"]
COPY ["/src/CatalogManagement.SharedKernel/CatalogManagement.SharedKernel.csproj", "CatalogManagement.SharedKernel/"]

RUN dotnet restore "CatalogManagement.Api/CatalogManagement.Api.csproj"
COPY ["src/", "/src/"]
WORKDIR /src/CatalogManagement.Api
RUN dotnet build "CatalogManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5154
ENV ASPNETCORE_URLS=http://+:5154
ENV ASPNETCORE_HTTP_PORTS=5154
CMD [ "dotnet","CatalogManagement.Api.dll" ]