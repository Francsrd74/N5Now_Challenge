
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccessControl.Api/AccessControl.Api.csproj", "AccessControl.Api/"]
RUN dotnet restore "AccessControl.Api/AccessControl.Api.csproj"
COPY . .
WORKDIR "/src/AccessControl.Api"
RUN dotnet build "AccessControl.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccessControl.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessControl.Api.dll"]