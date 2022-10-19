#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["demo/DemoPluginsNetCore7/DemoPluginsNetCore7.csproj", "demo/DemoPluginsNetCore7/"]
RUN dotnet restore "demo/DemoPluginsNetCore7/DemoPluginsNetCore7.csproj"
COPY . .
WORKDIR "/src/demo/DemoPluginsNetCore7"
RUN dotnet build "DemoPluginsNetCore7.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoPluginsNetCore7.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoPluginsNetCore7.dll"]