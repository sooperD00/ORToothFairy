# ORToothFairy API Dockerfile
# Place this file in your repo root (next to src/)

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy solution and project files first (better layer caching)
COPY src/ORToothFairy.Core/*.csproj src/ORToothFairy.Core/
COPY src/ORToothFairy.API/*.csproj src/ORToothFairy.API/
RUN dotnet restore src/ORToothFairy.API/ORToothFairy.API.csproj

# Copy everything else and build
COPY src/ORToothFairy.Core/ src/ORToothFairy.Core/
COPY src/ORToothFairy.API/ src/ORToothFairy.API/
RUN dotnet publish src/ORToothFairy.API/ORToothFairy.API.csproj -c Release -o /app --no-restore

# Runtime image (smaller)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

# DO App Platform uses PORT env var
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ORToothFairy.API.dll"]
