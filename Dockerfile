FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Instala o agente New Relic
RUN apt-get update && \
    apt-get install -y --no-install-recommends wget tar ca-certificates && \
    wget https://download.newrelic.com/dot_net_agent/latest_release/newrelic-dotnet-agent_linux_x64.tar.gz -O /tmp/newrelic.tar.gz && \
    mkdir -p /usr/local/newrelic-dotnet-agent && \
    tar -xzf /tmp/newrelic.tar.gz -C /usr/local/newrelic-dotnet-agent && \
    rm /tmp/newrelic.tar.gz && \
    apt-get remove -y wget && \
    apt-get autoremove -y && \
    rm -rf /var/lib/apt/lists/*

# Configura variáveis de ambiente do New Relic
ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER="{36032161-FFC0-4B61-B559-F6C5D41BAE5A}" \
    CORECLR_NEWRELIC_HOME="/usr/local/newrelic-dotnet-agent" \
    CORECLR_PROFILER_PATH="/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so" \
    NEW_RELIC_LICENSE_KEY="496842f48442728f2909046d53d679a9FFFFNRAL" \
    NEW_RELIC_APP_NAME="FCG.Api"

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG/FCG.csproj", "FCG/"]
COPY ["FCG.Application/FCG.Application.csproj", "FCG.Application/"]
COPY ["FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["FCG.Infrastructure/FCG.Infrastructure.csproj", "FCG.Infrastructure/"]
RUN dotnet restore "./FCG/FCG.csproj"
COPY . .
WORKDIR "/src/FCG"
RUN dotnet build "./FCG.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.dll"]
