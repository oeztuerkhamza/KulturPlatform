# ── Build Stage ──────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution & project files
COPY ["KulturPlatform.API/KulturPlatform.API.csproj",             "KulturPlatform.API/"]
COPY ["KulturPlatform.Application/KulturPlatform.Application.csproj", "KulturPlatform.Application/"]
COPY ["KulturPlatform.Domain/KulturPlatform.Domain.csproj",        "KulturPlatform.Domain/"]
COPY ["KulturPlatform.Infrastructure/KulturPlatform.Infrastructure.csproj", "KulturPlatform.Infrastructure/"]

RUN dotnet restore "KulturPlatform.API/KulturPlatform.API.csproj"

COPY . .
WORKDIR "/src/KulturPlatform.API"
RUN dotnet publish "KulturPlatform.API.csproj" -c Release -o /app/publish

# ── Runtime Stage ─────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install docker CLI for email account management
RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates curl gnupg && \
    install -m 0755 -d /etc/apt/keyrings && \
    curl -fsSL https://download.docker.com/linux/debian/gpg | gpg --dearmor -o /etc/apt/keyrings/docker.gpg && \
    echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/debian bookworm stable" > /etc/apt/sources.list.d/docker.list && \
    apt-get update && apt-get install -y --no-install-recommends docker-ce-cli && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Create uploads directory
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "KulturPlatform.API.dll"]
