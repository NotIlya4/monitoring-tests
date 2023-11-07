FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Restore
COPY Service.csproj /src/Service/
RUN dotnet restore /src/Service/

# Copy rest files
COPY . /src/Service/

# Build
RUN dotnet build /src/Service/Service.csproj -c Release -o /app/build/
RUN dotnet publish /src/Service/Service.csproj -c Release -o /app/publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

ENV ASPNETCORE_URLS=http://*:5000
EXPOSE 5000
COPY --from=build /app/publish /app/

ENTRYPOINT ["dotnet", "/app/Service.dll"]