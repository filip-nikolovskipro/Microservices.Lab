FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ApiGateways/ApiGw-Base/OcelotApiGw.csproj", "src/ApiGateways/ApiGw-Base/"]
RUN dotnet restore "src/ApiGateways/ApiGw-Base/OcelotApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/ApiGw-Base"
RUN dotnet build "OcelotApiGw.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OcelotApiGw.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]
