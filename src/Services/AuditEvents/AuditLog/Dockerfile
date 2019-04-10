FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Services/AuditEvents/AuditLog/AuditLog.API.csproj", "src/Services/AuditEvents/AuditLog/"]
RUN dotnet restore "src/Services/AuditEvents/AuditLog/AuditLog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/AuditEvents/AuditLog"
RUN dotnet build "AuditLog.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuditLog.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuditLog.API.dll"]
