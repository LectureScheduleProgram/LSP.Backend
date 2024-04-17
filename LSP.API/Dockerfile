FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LSP.API/LSP.API.csproj", "LSP.API/"]
COPY ["LSP.Business/LSP.Business.csproj", "LSP.Business/"]
COPY ["LSP.Core/LSP.Core.csproj", "LSP.Core/"]
COPY ["LSP.Dal/LSP.Dal.csproj", "LSP.Dal/"]
COPY ["LSP.Entity/LSP.Entity.csproj", "LSP.Entity/"]
RUN dotnet restore "LSP.API/LSP.API.csproj"
COPY . .
WORKDIR "/src/LSP.API"
RUN dotnet build "LSP.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LSP.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LSP.API.dll"]
