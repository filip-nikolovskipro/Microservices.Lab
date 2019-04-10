FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ApiGateways/Web.Bff.Record/aggregator/Web.Record.HttpAggregator/Web.Record.HttpAggregator.csproj", "src/ApiGateways/Web.Bff.Record/aggregator/Web.Record.HttpAggregator/"]
RUN dotnet restore "src/ApiGateways/Web.Bff.Record/aggregator/Web.Record.HttpAggregator/Web.Record.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Web.Bff.Record/aggregator/Web.Record.HttpAggregator"
RUN dotnet build "Web.Record.HttpAggregator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Web.Record.HttpAggregator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.Record.HttpAggregator.dll"]
