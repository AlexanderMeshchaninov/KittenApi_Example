FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["WebApiAbstraction/WebApiAbstraction.csproj", "WebApiAbstraction/"]
COPY ["WebApiAuthService/WebApiAuthService.csproj", "WebApiAuthService/"]
COPY ["WebApiRepository/WebApiRepository.csproj", "WebApiRepository/"]
COPY ["WebApiDataLayer/WebApiDataLayer.csproj", "WebApiDataLayer/"]
COPY ["WebApiRepositoryAbstraction/WebApiRepositoryAbstraction.csproj", "WebApiRepositoryAbstraction/"]
COPY ["WebApiBusinessLayer/WebApiBusinessLayer.csproj", "WebApiBusinessLayer/"]
COPY ["WebApiBusinessLayerAbstraction/WebApiBusinessLayerAbstraction.csproj", "WebApiBusinessLayerAbstraction/"]
COPY ["WebApiFluentValidation/WebApiFluentValidation.csproj", "WebApiFluentValidation/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
