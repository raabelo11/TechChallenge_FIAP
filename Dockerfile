# Imagem base com ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Instala o agente New Relic (mais robusto e com validações)
RUN apt-get update && \
    apt-get install -y --no-install-recommends wget ca-certificates gnupg && \
    wget -O newrelic-install.sh https://download.newrelic.com/install/newrelic-cli/scripts/install.sh && \
    chmod +x newrelic-install.sh && \
    ./newrelic-install.sh install -n dotnet-agent && \
    rm -rf /var/lib/apt/lists/* newrelic-install.sh

# Variáveis de ambiente do New Relic com a chave fixada
ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER="{36032161-FFC0-4B61-B559-F6C5D41BAE5A}" \
    CORECLR_NEWRELIC_HOME="/usr/local/newrelic-dotnet-agent" \
    CORECLR_PROFILER_PATH="/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so" \
    NEW_RELIC_LICENSE_KEY="496842f48442728f2909046d53d679a9FFFFNRAL" \
    NEW_RELIC_APP_NAME="FCG.Api"

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build
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

# Fase de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.dll"]
