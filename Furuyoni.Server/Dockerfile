FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Furuyoni.Server/Furuyoni.Server.csproj", "Furuyoni.Server/"]
RUN dotnet restore "Furuyoni.Server/Furuyoni.Server.csproj"
COPY . .
WORKDIR "/src/Furuyoni.Server"
RUN dotnet build "Furuyoni.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Furuyoni.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Furuyoni.Server.dll"]
