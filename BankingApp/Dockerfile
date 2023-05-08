FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BankingApp/BankingApp.fsproj", "BankingApp/"]
RUN dotnet restore "BankingApp/BankingApp.fsproj"
COPY . .
WORKDIR "/src/BankingApp"
RUN dotnet build "BankingApp.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingApp.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingApp.dll"]
