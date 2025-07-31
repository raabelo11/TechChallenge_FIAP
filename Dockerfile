FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build (mantida igual - usa SDK)
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

# Fase de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final (Amazon Linux)
FROM base AS final
WORKDIR /app

# Instalação para Amazon Linux (yum/dnf)
RUN yum install -y tar gzip wget && \
    wget https://download.newrelic.com/dot_net_agent/latest_release/newrelic-dotnet-agent_linux_x64.tar.gz && \
    mkdir -p /usr/local/newrelic-dotnet-agent && \
    tar -xzf newrelic-dotnet-agent_linux_x64.tar.gz -C /usr/local/newrelic-dotnet-agent && \
    rm newrelic-dotnet-agent_linux_x64.tar.gz && \
    yum clean all

# Variáveis de ambiente (obrigatórias)
ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
    CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
    CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
    NEW_RELIC_LICENSE_KEY=496842f48442728f2909046d53d679a9FFFFNRAL \
    NEW_RELIC_APP_NAME="FCG.Api"

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.dll"]
