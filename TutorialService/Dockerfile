#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TutorialService/TutorialService.csproj", "TutorialService/"]
RUN dotnet restore "TutorialService/TutorialService.csproj"
COPY . .
WORKDIR "/src/TutorialService"
RUN dotnet build "TutorialService.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "TutorialService/DBMigrator.csproj"
COPY . .
WORKDIR "/src/DBMigrator"
RUN dotnet build "DBMigrator.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "TutorialService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TutorialService.dll"]