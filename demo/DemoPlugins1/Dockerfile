#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["demo/DemoPlugins1/DemoPlugins1.csproj", "demo/DemoPlugins1/"]
RUN dotnet restore "demo/DemoPlugins1/DemoPlugins1.csproj"
COPY . .
WORKDIR "/src/demo/DemoPlugins1"
RUN dotnet build "DemoPlugins1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoPlugins1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoPlugins1.dll"]