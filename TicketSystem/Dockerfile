FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TicketSystem/TicketSystem.fsproj", "TicketSystem/"]
RUN dotnet restore "TicketSystem/TicketSystem.fsproj"
COPY . .
WORKDIR "/src/TicketSystem"
RUN dotnet build "TicketSystem.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketSystem.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketSystem.dll"]
